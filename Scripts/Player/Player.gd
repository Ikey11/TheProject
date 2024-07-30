extends CharacterBody2D

const SPEED = 300.0
const DASH_SPEED = 600.0
const DASH_DURATION = 0.2

var is_dashing = false
var dash_time_left = 0.0
var dash_direction = Vector2.ZERO

func _physics_process(delta):
	# Point cursor
	var mouse_pos = get_global_mouse_position()
	$Fulcrum.look_at(mouse_pos)
	
	# Get the input direction and handle the movement/deceleration.
	var direction = Input.get_vector("Left", "Right", "Up", "Down")
	
	if Input.is_action_just_pressed("ui_select") and not is_dashing:
		start_dash((mouse_pos - global_position).normalized())

	if is_dashing:
		handle_dash(delta)
	else:
		velocity = direction * SPEED

	move_and_slide()

func handle_dash(delta: float):
	dash_time_left -= delta
	if dash_time_left <= 0:
		is_dashing = false
		velocity = Vector2.ZERO
	else:
		velocity = dash_direction * DASH_SPEED

func start_dash(direction: Vector2):
	if direction == Vector2.ZERO:
		direction = Vector2.RIGHT.rotated(global_rotation)

	dash_direction = direction.normalized()
	dash_time_left = DASH_DURATION
	is_dashing = true
