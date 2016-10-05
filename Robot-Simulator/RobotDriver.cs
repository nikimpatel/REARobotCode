using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Simulator
{
    public class RobotDriver
    {

        public RobotDriver(Robot robot)
        {
            this.Robot = robot;
        }
        public Robot Robot { get; set; }
        #region Methods
        //execute command for robot
        public string executecommand(string command)
        {
            string response = "";
            InstructionArguments args = null;
            //get instruction name from command and arguments to check valid instruction has been placed.
            var instruction = getInstruction(command, ref args);
            switch(instruction)
            {
                case Instruction.Place:
                    var placearg = (PlaceInstructionArguments)args;
                    if(this.Robot.place(placearg.X, placearg.Y, placearg.Facing))
                    {
                        response = "Done.";
                    }
                    else
                    {
                        response = Robot.LastError;
                    }
                    break;
                case Instruction.Move:
                    if(this.Robot.move())
                    {
                        response = "Done.";
                    }
                    else
                    {
                        response = Robot.LastError;
                    }
                    break;
                case Instruction.Left:
                    if (this.Robot.left())
                    {
                        response = "Done.";
                    }
                    else
                    {
                        response = Robot.LastError;
                    }
                    break;
                case Instruction.Right:
                    if (this.Robot.right())
                    {
                        response = "Done.";
                    }
                    else
                    {
                        response = Robot.LastError;
                    }
                    break;
                case Instruction.Report:
                    response = Robot.Report();                    
                    break;
                default:
                    response = "Invalid command";
                    break;


            }
            return response;
        }
        //parsing the command into argument and command then return the instruction 
        private Instruction getInstruction(string command,ref InstructionArguments args)
        {
            Instruction result;
            int argstringposindex = command.IndexOf(" ");
            string argstring="";

            if (argstringposindex >0)
            {
                //split the command name and paramaeter
                argstring = command.Substring(argstringposindex + 1);
                command = command.Substring(0, argstringposindex);
                
            }
            command = command.ToUpper();
            //parse the command
            if (Enum.TryParse(command, true, out result))
            {
                if (result == Instruction.Place)
                {
                     //parse the place command arguments
                    if (!TryParsePlaceArgs(argstring, ref args))
                    {
                        result = Instruction.Invalid;
                    }
                }
            }
            else
                result = Instruction.Invalid;

            return result;
           
        }
        //parse all the command arguments
        private bool TryParsePlaceArgs(string argString, ref InstructionArguments args)
        {
            var argParts = argString.Split(',');
            int x, y;
            Facing facing;

            if (argParts.Length == 3 &&
                TryGetCoordinate(argParts[0], out x) &&
                TryGetCoordinate(argParts[1], out y) &&
                TryGetFacingDirection(argParts[2], out facing))
            {
                args = new PlaceInstructionArguments
                {
                    X = x,
                    Y = y,
                    Facing = facing,
                };
                return true;
            }
            return false;
        }
        //parse the cordinate string to integer
        private bool TryGetCoordinate(string coordinate, out int coordinateValue)
        {
            return int.TryParse(coordinate, out coordinateValue);
        }
        //parse the direction string value to facing enum.
        private bool TryGetFacingDirection(string direction, out Facing facing)
        {
            return Enum.TryParse<Facing>(direction, true, out facing);
        }
        #endregion
    }
}
