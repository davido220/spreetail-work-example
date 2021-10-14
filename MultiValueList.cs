using System.Collections.Generic;
using System.Linq;

namespace SpreetailWorkSampleDavidOBrien
{
    public class MultiValueList
    {
        public List<MultiValue> Items { get; set; } = new List<MultiValue>();

        public void Add(string key, string value)
        {
            if (Items.Where(o => o.Key == key).Count() == 0)
            {
                Items.Add(new MultiValue { Key = key });
            }
            var itemToBeUpdated = Items.FirstOrDefault(o => o.Key == key);
            if (itemToBeUpdated != null && !itemToBeUpdated.Values.Contains(value))
            {
                itemToBeUpdated.Values.Add(value);
            }
        }

        public void Remove(string key)
        {
            Items.Remove(Items.FirstOrDefault(o => o.Key == key));
        }

        public void Remove(string key, string value)
        {
            Items.FirstOrDefault(o => o.Key == key).Values.Remove(value);

            // Key cleanup
            if (Items.Where(o => o.Key == key && o.Values.Count() == 0).Any())
            {
                Items.Remove(Items.FirstOrDefault(o => o.Key == key));
            }
        }
    }
}
