﻿using Improbable.Fps.Custommovement;
using Improbable.Gdk.Movement;
using Improbable.Gdk.StandardTypes;
using UnityEngine;

// Checks newstate velocity vs actual character movement, sets IsGrounded = true not all desired vertical movement
// happened (because we hit the ground).
public class IsGroundedMovement : MyMovementUtils.IMovementProcessorOLD
{
    private const float DeltaThreshold = 0.1f;

    public bool Process(CustomInput input, MovementState previousState,
        ref MovementState newState, float deltaTime)
    {
        var desiredMovement = (newState.Velocity.ToVector3() * deltaTime).y;
        var actualMovement = (newState.Position.ToVector3() - previousState.Position.ToVector3()).y;
        var delta = actualMovement - desiredMovement;

        newState.IsGrounded = (Mathf.Abs(delta / actualMovement) > DeltaThreshold && delta > 0);

        return true;
    }
}