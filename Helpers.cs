using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SC = SpreetailWorkSampleDavidOBrien.StringConsts;

namespace SpreetailWorkSampleDavidOBrien
{
    public static class Helpers
    {
        // Returns a list of commands that are only valid for one input separated by whitespace (including the command)
        public static List<CommandEnum> OnePartCommands = new List<CommandEnum>
        {
            CommandEnum.ALLMEMBERS,
            CommandEnum.CLEAR,
            CommandEnum.ITEMS,
            CommandEnum.KEYS  
        };

        // Returns a list of commands that are only valid for two inputs separated by whitespace (including the command)
        public static List<CommandEnum> TwoPartCommands = new List<CommandEnum>
        {
            CommandEnum.KEYEXISTS,
            CommandEnum.MEMBERS,
            CommandEnum.REMOVEALL
        };

        // Returns a list of commands that are only valid for three inputs separated by whitespace (including the command)
        public static List<CommandEnum> ThreePartCommands = new List<CommandEnum>
        {
            CommandEnum.ADD,
            CommandEnum.MEMBEREXISTS,
            CommandEnum.REMOVE
        };
        
        // Returns a comma separated string of all valid command names
        public static string GetCommandEnumTypesAsString()
        {
            string result;
            var stringBuilder = new StringBuilder();
            var commandEnumTypesStringArray = Enum.GetNames(typeof(CommandEnum)).Where(o => o != CommandEnum.DEFAULT.ToString());

            foreach(var item in commandEnumTypesStringArray)
            {
                stringBuilder.Append($"{item}, ");
            }

            result = stringBuilder.ToString().TrimEnd(' ').TrimEnd(',');

            return result;
        }

        // Returns documentation for the given command
        // commandEnum: command entered
        public static string GetCommandInfo(CommandEnum commandEnum)
        {
            var result = string.Empty;

            switch (commandEnum) {
                case CommandEnum.ADD:
                    result = SC.ADD_INFO;
                    break;
                case CommandEnum.ALLMEMBERS:
                    result = SC.ALLMEMBERS_INFO;
                    break;
                case CommandEnum.CLEAR:
                    result = SC.CLEAR_INFO;
                    break;
                case CommandEnum.ITEMS:
                    result = SC.ITEMS_INFO;
                    break;
                case CommandEnum.KEYEXISTS:
                    result = SC.KEYEXISTS_INFO;
                    break;
                case CommandEnum.KEYS:
                    result = SC.KEYS_INFO;
                    break;
                case CommandEnum.MEMBEREXISTS:
                    result = SC.MEMBEREXISTS_INFO;
                    break;
                case CommandEnum.MEMBERS:
                    result = SC.MEMBERS_INFO;
                    break;
                case CommandEnum.REMOVE:
                    result = SC.REMOVE_INFO;
                    break;
                case CommandEnum.REMOVEALL:
                    result = SC.REMOVEALL_INFO;
                    break;
                default:
                    result = SC.INVALID_COMMAND;
                    break;
            }
            
            return result;
        }

        // Parses inputs to key and value
        // inputs: string list representation of user input value
        // OUT key: returns the key for the given inputs (if any)
        // OUT value: returns the value for the given inputs (if any)
        public static void GetParamsFromInputs(List<string> inputs, out string key, out string value)
        {
            key = null;
            value = null;

            if (inputs.Count > 1)
            {
                key = inputs[1];
            }

            if (inputs.Count > 2)
            {
                value = inputs[2];
            }
        }

        // Returns true for invalid command
        // inputParameters: trimmed list of strings from user entered value
        // OUT commandEnum: user entered command, DEFAULT for invalid commands
        // OUT isCommandInquiry: returns true if user entered "?" after command, else false
        public static bool IsCommandInvalid(List<string> inputParamaters, out CommandEnum commandEnum, out bool isCommandInquiry)
        {
            // Defensive code follows...
            commandEnum = CommandEnum.DEFAULT;
            isCommandInquiry = false;
            // Command is INVALID, input is empty
            if (inputParamaters == null || inputParamaters.Count == 0 || string.IsNullOrWhiteSpace(inputParamaters[0]))
            {
                return false;
            }
            // Command is INVALID, cannot parse CommandEnum
            else if (!Enum.TryParse(inputParamaters[0], true, out commandEnum) || !Enum.IsDefined<CommandEnum>(commandEnum))
            {
                return true;
            }
            // Command is VALID, "<Command> ?" was entered
            else if (inputParamaters.Count == 2 && inputParamaters[1] == SC.QUESTION_MARK)
            {
                isCommandInquiry = true;
                return false;
            }
            // Command is INVALID, inputParamaters.Count is incorrect for this CommandEnum member
            else if ((Helpers.OnePartCommands.Contains(commandEnum) && inputParamaters.Count != 1) ||
                (Helpers.TwoPartCommands.Contains(commandEnum) && inputParamaters.Count != 2) ||
                (Helpers.ThreePartCommands.Contains(commandEnum) && inputParamaters.Count != 3))
            {
                return true;
            }

            // Command is VALID, all checks passed
            return false;
        }

        // Format input into a list of strings that have been trimmed
        // input: user entered value
        public static List<string> GetStringsFromInput(string input)
        {
            var results = new List<string>();
            // Split input on whitespace(s)
            var inputArray = input.Split(' ');

            // Parse string[] to List<string> while trimming each string
            foreach (var inputProperty in inputArray)
            {
                results.Add(inputProperty.Trim());
            }

            return results;
        }
    }
}
