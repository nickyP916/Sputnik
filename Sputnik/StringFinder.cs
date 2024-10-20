namespace Sputnik
{
    internal class StringFinder
    {
        internal static bool Find(string letters, string toFind)
        {
            if (String.IsNullOrEmpty(toFind))
                return false;

            var letterList = letters.ToUpper().ToList();
            foreach (var l in toFind.ToUpper())
            {
                var foundIndex = letterList.IndexOf(l);
                if (foundIndex == -1)
                    return false;
                letterList.RemoveAt(foundIndex);
            }
            return true;
        }
    }
}
