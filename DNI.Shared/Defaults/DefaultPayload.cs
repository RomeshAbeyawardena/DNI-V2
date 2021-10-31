using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Defaults
{
    public class DefaultPayload : IPayload
    {
        public string this[int index] { get => Parameters.ElementAtOrDefault(index); }
        public string Name { get; internal set; }
        public IEnumerable<string> Parameters { get; internal set; }
        public int Length => Parameters.Count();

        public IEnumerator<string> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new($"Name: {Name}\r\nParameters:\r\n");
            var index = 0;
            foreach (var parameter in Parameters)
            {
                stringBuilder.AppendLine($"[{index}]: {parameter}");
                index++;
            }

            return stringBuilder.ToString();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
