using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_14
{
    enum MoveOrientation { North, West, South,  East }
    enum MoveDirection { Left, Right }
    class Move
    {
        public int MoveNo;
        public MoveOrientation MoveOrientation;
        public MoveDirection MoveDirection;
        public List<string> Data;
        public Move(int moveNo, MoveOrientation orientation, MoveDirection direction, List<string> data)
        {
            MoveNo = moveNo;
            MoveOrientation = orientation;
            MoveDirection = direction;
            Data = data;

        }
    }
}

