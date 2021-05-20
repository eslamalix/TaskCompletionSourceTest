using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCompletionSourceTest
{
//var collection = new string[] { ... };
//var config = new EnumeratorConfig
//{
//    MinLength = 3,
//    MaxLength = 10,
//    StartWithCapitalLetter = true
//};

//// The custom enumerator will return strings that are longer than or equal to 3 characters
//// and shorter than or equal to 10 characters, and start with a capital letter.
//var enumerator = new CustomStringEnumerator(collection, config);
//foreach (var s in enumerator)
//{
//    Console.WriteLine(s);
//}
//CustomStringEnumerator can be used to enumerate any object that implements IEnumerable<string>
//interface. It must not change the order of elements in the collection being enumerated.
//Assume that a null string is a string of length 0. Under the hood, CustomStringEnumerator
//filters elements of the collection according to the provided rules.
//These rules are specified in the EnumeratorConfig class. CustomStringEnumerator should be defined as follows:

    internal class CustomStringEnumerator : IEnumerable<string>
    {
        private readonly IEnumerable<string> _listOfString;

        /// <summary> Constructor </summary>
        /// <exception cref="System.ArgumentNullException">If a collection is null</exception>
        /// <exception cref="System.ArgumentNullException">If a config is null</exception>
        public CustomStringEnumerator(IEnumerable<string> collection, EnumeratorConfig config)
        {
            if (config.StartWithCapitalLetter==true)
            {
                _listOfString = collection.Where(s => s.Length >= config.MinLength&&s.Length <= config.MaxLength&&char.IsUpper(s[0]));
            }

            else
            {
                _listOfString = collection.Where(s => s.Length >= config.MinLength&&s.Length <= config.MaxLength&&char.IsLower(s[0]));
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _listOfString.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _listOfString.GetEnumerator();
        }
    }

    public class EnumeratorConfig
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool StartWithCapitalLetter { get; set; }
    }
}