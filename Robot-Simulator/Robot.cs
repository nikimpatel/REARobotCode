using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Simulator
{
    public class Robot
    {
        public string LastError { get; set; }
        public Robot()
        {
            LastError = "";
        }
        #region fields
        private const int TABLE_SIZE = 5;
        private int? _x;
        private int? _y;
        private Facing _facing;
        #endregion
        #region methods
        // place command to place robot.
        public bool place(int x, int y, Facing facing)
        {
            //before place check the valid input to place
            if (ValidateOnTable(x, y, "place"))
            {
                _x = x;
                _y = y;
                _facing = facing;
                return true;
            }
            return false;
        }
        //move command to move robot.
        public bool move()
        {
            //before move check the constraint for moving
            if (MandateIsPlaced("move"))
            {
                int newx = GetXAfterMove();
                int newy = GetYAfterMove();
                if (ValidateOnTable(newx, newy, "moved"))
                {
                    _x = newx;
                    _y = newy;
                    return true;
                }
            }
            return false;

        }
        //report command to report robot and get final position.
        public string Report()
        {
            if (MandateIsPlaced("report it's position"))
            {
                return String.Format("{0},{1},{2}", _x.Value, _y.Value, _facing.ToString().ToUpper());
            }
            return "";
        }
        //left command to turn left for robot.
        public bool left()
        {
            return changeDirection(Direction.Left);
        }
        //right command to turn right for robot.
        public bool right()
        {
            return changeDirection(Direction.Right);
        }
        //validate the postition and change direction of robot.
        public bool changeDirection(Direction direction)
        {
            //before turn check the constraint
            if (MandateIsPlaced("turn"))
            {
                int facingnum = (int)_facing;
                
                    var facingAsNumber = (int)_facing;
                    facingAsNumber += 1 * (direction == Direction.Right ? 1 : -1);
                    if (facingAsNumber == 5) facingAsNumber = 1;
                    if (facingAsNumber == 0) facingAsNumber = 4;
                    _facing = (Facing)facingAsNumber;
                    return true;

                
            }

            return false;
        }
        //check the valid input to put the robot on table
        private bool ValidateOnTable(int x, int y, string action)
        {
            if (x < 0 || y < 0 || x > TABLE_SIZE || y > TABLE_SIZE)
            {
                LastError = String.Format("Robot cannot be {0} there.", action);
                return false;
            }
            return true;
        }
        //check the robot has been placed on before move
        private bool MandateIsPlaced(string action)
        {
            if (!_x.HasValue || !_y.HasValue)
            {
                LastError = String.Format("Robot cannot {0} until it has been placed on the table.", action);
                return false;
            }
            return true;

        }
        //get the x value after each move
        private int GetXAfterMove()
        {
            if (_facing == Facing.East)
            {
                return _x.Value + 1;
            }
            else
            {
                if (_facing == Facing.West)
                {
                    return _x.Value - 1;
                }
            }
            return _x.Value;
        }
        //get the Y value after each move
        private int GetYAfterMove()
        {
            if (_facing == Facing.North)
            {
                return _y.Value + 1;
            }
            else
            {
                if (_facing == Facing.South)
                {
                    return _y.Value - 1;
                }
            }
            return _y.Value;
        }
        #endregion
    }
}
