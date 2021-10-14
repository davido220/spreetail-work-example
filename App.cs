using System;
using System.Collections.Generic;
using System.Linq;
using SC = SpreetailWorkSampleDavidOBrien.StringConsts;

namespace SpreetailWorkSampleDavidOBrien
{
    public class App
    {
        // Used to limit the maximum nuber of ReadLine iterations
        private readonly int _limit;
        // Used to store the Multi-Value Dictionary values
        private MultiValueList appMultiValueDictionary = new MultiValueList();

        // CTOR
        // limit: maximum nuber of ReadLine iterations
        public App(int limit)
        {
            // Set limit at the App class level
            _limit = limit;
        }

        // Run App
        public void Run()
        {
            // Output instructions
            Console.WriteLine(SC.COMMAND_INTRO);
            Console.WriteLine(Helpers.GetCommandEnumTypesAsString());
            Console.WriteLine(SC.COMMAND_INFO);

            // Listen for user input after each ProcessCommand completes
            for (int i = 0; i < _limit; i++)
            {
                var results = ProcessCommand(Console.ReadLine());
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }

            Environment.Exit(0);
        }

        // Process the user entered string as a command
        // input: user entered value
        public List<string> ProcessCommand(string input)
        {
            var results = new List<string>();
            CommandEnum commandEnum;
            string key;
            string value;
            bool isCommandInquiry;
            
            // Convert user input to list of trimmed strings
            var inputs = Helpers.GetStringsFromInput(input);

            // Command is invalid
            if (Helpers.IsCommandInvalid(inputs, out commandEnum, out isCommandInquiry))
            {
                results.Add(SC.INVALID_COMMAND);

                return results;
            }

            // Parse user inputs to key & value
            Helpers.GetParamsFromInputs(inputs, out key, out value);

            // User entered command inquiry, return documentation for the command
            if (isCommandInquiry)
            {
                results.Add(Helpers.GetCommandInfo(commandEnum));

                return results;
            }
            
            // Take appropriate action for a given command...
            // ...(could have used a base abstract class or a generic pattern to make this cleaner but it would ultimately be much more code and complexity and it's very readable this way)
            switch (commandEnum) {
                case CommandEnum.ADD:
                    results.AddRange(Add(key, value));
                    break;
                case CommandEnum.ALLMEMBERS:
                    results.AddRange(AllMembers());
                    break;
                case CommandEnum.CLEAR:
                    results.AddRange(Clear());
                    break;
                case CommandEnum.ITEMS:
                    results.AddRange(Items());
                    break;
                case CommandEnum.KEYEXISTS:
                    results.AddRange(KeyExists(key));
                    break;
                case CommandEnum.KEYS:
                    results.AddRange(Keys());
                    break;
                case CommandEnum.MEMBEREXISTS:
                    results.AddRange(MemberExists(key, value));
                    break;
                case CommandEnum.MEMBERS:
                    results.AddRange(Members(key));
                    break;
                case CommandEnum.REMOVE:
                    results.AddRange(Remove(key, value));
                    break;
                case CommandEnum.REMOVEALL:
                    results.AddRange(RemoveAll(key));
                    break;
                case CommandEnum.GETINTERSECTVALUESFORKEYS:
                    results.AddRange(GetIntersectValuesForKey(new List<string>{ key }));
                    break;
                default:
                    results.Add(SC.INVALID_COMMAND);
                    break;
            }

            return results;
        }

        #region execute-commands

        public List<Tuple<string, string>> GetIntersectValuesForKey(List<string> keys)
        {
            var results = new List<string>();

            var myColl = new List<Tuple<string, string>>();

            foreach (var item in appMultiValueDictionary.Items.Where(o => keys.Contains(o.Key)))
            {
                foreach (var value in item.Values)
                {
                    myColl.Add(new Tuple<string, string>(item.Key, value));
                }
            }

            var myColl2 = new List<Tuple<string, string>>();
            
            foreach (var item in myColl)
            {
                if (myColl.Where(o => o.Item2 == item.Item2 && o.Item1 != item.Item1).Any())
                {
                    myColl2.Add(item);
                }
            }

            var myColl3 = new List<Tuple<string, string>>();
            
            foreach(var item in myColl2)
            {
                if (!myColl3.Contains(item))
                {
                    myColl3.Add(item);
                }
            }

            myColl3 = myColl3.OrderBy(o => o.Item1).OrderBy(o => o.Item2).ToList();

            return myColl3;
        }

        // Adds a member to a collection for a given key. Displays an error if the member already exists for the key.
        public List<string> Add(string key, string value)
        {
            var results = new List<string>();

            if (appMultiValueDictionary.Items.Where(o => o.Key == key && o.Values.Contains(value)).Any())
            {
                results.Add(SC.ERROR_MEMBER_ALREADY_EXISTS_FOR_KEY);

                return results;
            }

            appMultiValueDictionary.Add(key, value);
            results.Add(SC.ADDED);

            return results;
        }
        
        // Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        public List<string> AllMembers()
        {
            var results = new List<string>();

            foreach (var item in appMultiValueDictionary.Items.SelectMany(o => o.Values))
            {
                results.Add(item);
            }

            return results;
        }
        
        // Removes all keys and all members from the dictionary.
        public List<string> Clear()
        {
            var results = new List<string>();

            appMultiValueDictionary = new MultiValueList();
            results.Add(SC.CLEARED);

            return results;
        }
        
        // Returns all keys in the dictionary and all of their members. Returns nothing if there are none. Order is not guaranteed.
        public List<string> Items()
        {
            var results = new List<string>();

            foreach (var item in appMultiValueDictionary.Items)
            {
                foreach (var value in item.Values)
                {
                    results.Add($"{item.Key}: {value}");
                }
            }

            return results;
        }
        
        // Returns whether a key exists or not.
        public List<string> KeyExists(string key)
        {
            var results = new List<string>();

            results.Add(KeyExistsBool(key).ToString().ToLowerInvariant());

            return results;
        }

        // Returns true/false whether a key exists or not.
        public bool KeyExistsBool(string key)
        {
            return appMultiValueDictionary.Items.Where(o => o.Key == key).Any();
        }
        
        // Returns all the keys in the dictionary. Order is not guaranteed.
        public List<string> Keys()
        {
            var results = new List<string>();

            foreach (var item in appMultiValueDictionary.Items)
            {
                results.Add(item.Key);
            }

            return results;
        }
        
        // Returns whether a member exists within a key. Returns false if the key does not exist.
        public List<string> MemberExists(string key, string value)
        {
            var results = new List<string>();

            results.Add(MemberExistsBool(key, value).ToString().ToLowerInvariant());

            return results;
        }

        // Returns true/false whether a member exists within a key. Returns false if the key does not exist.
        public bool MemberExistsBool(string key, string value)
        {
            return appMultiValueDictionary.Items.Where(o => o.Key == key && o.Values.Contains(value)).Any();
        }
        
        // Returns the collection of strings for the given key. Return order is not guaranteed. Returns an error if the key does not exists.
        public List<string> Members(string key)
        {
            var results = new List<string>();
            var matchesForKey = appMultiValueDictionary.Items.Where(o => o.Key == key);

            if (matchesForKey.Count() == 0)
            {
                results.Add(SC.ERROR_KEY_DOES_NOT_EXIST);

                return results;
            }

            foreach (var item in matchesForKey)
            {
                foreach (var value in item.Values)
                {
                    results.Add(value);
                }
            }

            return results;
        }
        
        // Removes a member from a key. If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.
        public List<string> Remove(string key, string value)
        {
            var results = new List<string>();

            if (!KeyExistsBool(key))
            {
                results.Add(SC.ERROR_KEY_DOES_NOT_EXIST);

                return results;
            }
            else if (!MemberExistsBool(key, value))
            {
                results.Add(SC.ERROR_MEMBER_DOES_NOT_EXIST);

                return results;
            }

            appMultiValueDictionary.Remove(key, value);

            results.Add(SC.REMOVED);

            return results;
        }
        
        // Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        public List<string> RemoveAll(string key)
        {
            var results = new List<string>();

            if (!KeyExistsBool(key))
            {
                results.Add(SC.ERROR_KEY_DOES_NOT_EXIST);

                return results;
            }

            appMultiValueDictionary.Items.Remove(appMultiValueDictionary.Items.FirstOrDefault(o => o.Key == key));

            results.Add(SC.REMOVED);

            return results;
        }

        #endregion
    }
}
