using System;

[Flags]
public enum Direction : byte
{
    None = 0,   // 0000 0000
    North = 1,  // 0000 0001
    East = 2,   // 0000 0010
    South = 4,  // 0000 0100
    West = 8,   // 0000 1000
}

public enum TestDirection : byte
{
    None = 0,   // 0000 0000
    North = 1,  // 0000 0001
    East = 2,   // 0000 0010
    South = 4,  // 0000 0100
    West = 8,   // 0000 1000
}