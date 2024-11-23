namespace Sputnik
{
    internal class WordValidator
    {
        internal static async Task<bool> Validate(string wordToCheck, string letters)
        {
            if (String.IsNullOrEmpty(wordToCheck)) 
                return false;

            if (wordToCheck.Length == 1 || wordToCheck.Length > letters.Length)
                return false;

            var existsInLetters = StringFinder.Find(letters, wordToCheck);
            if (!existsInLetters) 
                return false;

            return await CheckWordExists(wordToCheck);
        }

        private static async Task<bool> CheckWordExists(string word)
        {
            var url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";

            using var client = new HttpClient();
            try
            {
                var result = await client.GetAsync(url);
                if(result.StatusCode == System.Net.HttpStatusCode.NotFound) 
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                //Todo: log
                throw;
            }
        }
    }
}
