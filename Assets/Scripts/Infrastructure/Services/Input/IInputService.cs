﻿using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 MoveDirection { get; }
    }
}
