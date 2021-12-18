using System;

namespace regex
{
    class Program
    {
        static string[] PunctuationRemover(string[] words){

            char[] punctuations = new char[]{'.', ',', '!', '?', '(', ')', '"', '-', ':', ';', '[', ']'};

            for (int i = 0; i < words.Length; i++)
            {
                bool exception_escape = false;
                for (int j = 0; j < words[i].Length; j++)
                {
                    for (int k = 0; k < 12; k++)
                    {
                        if(words[i][j] == punctuations[k]){
                            words[i] = words[i].Remove(j,1);
                            exception_escape = true;
                            break;
                        }
                    }
                    if(exception_escape){
                        break;
                    }
                }
            }

            return words;
        }
        static string[] FoundScore(string[] words, string pattern, int word_count){
            string[] elligable_words = new string[word_count];
            
            
            for (int i = 0; i < word_count; i++)
            {
                bool flag = true;
                if(words[i].Length == pattern.Length){
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if(pattern[j] != '-' && pattern.ToLower()[j] != words[i].ToLower()[j]){
                            flag = false;
                        }
                    }
                    if(flag){
                        elligable_words[i] = words[i];
                    }
                }
            }

            return elligable_words;
        }
        
        static void Main(string[] args){
            Console.Clear();

            string input_text;
            string pattern;
            int word_count;            

            input_text = "Miss Polly had a poor dolly, who was sick. She called for the talled doctor Solly to come quick. He knocked on the DOOR like a actor in the healthcare sector.";
            pattern = "-olly";           
            
            string[] words = input_text.Split();

            word_count = words.Length;

            string[] final_words = new string[word_count];
            bool IsPatternContainsScore = false;

            for(int i = 0; i < pattern.Length; i++ ){
                if(pattern[i] == '-'){
                    IsPatternContainsScore = true;
                }
                else if(pattern[i] == '*'){
                }
            }

            words = PunctuationRemover(words);
            if(IsPatternContainsScore){
                final_words = FoundScore(words, pattern, word_count);
            }
            for (int i = 0; i < word_count; i++)
            {
                if(final_words[i] != null){
                    System.Console.WriteLine(final_words[i]);
                }
            }
        }   
    }
}

