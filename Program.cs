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
        static int CountWords(string input_text){
            int counter = 0;
            for(int i = 0; i < input_text.Length; i++){
                if(input_text[i] == ' '){
                    counter++;
                }
            }
            return counter;
        }
        static string[] FoundScore(string[] words, string pattern, int word_count){
            string[] elligable_words = new string[word_count];
            
            for (int i = 0; i < word_count; i++)
            {
                bool flag = true;
                if(words[i].Length == pattern.Length){
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if(pattern[j] != '-' && pattern[j] != words[i][j]){
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
            string input_text;
            string pattern;
            int word_count;
            Console.Clear();
            input_text = "Miss Polly had a poor dolly, who was sick. She called for the talled doctor Solly to come quick. He knocked on the DOOR like a actor in the healthcare sector.";
            pattern = "-olly";

            bool[] score_index = new bool[pattern.Length];
            bool[] star_index = new bool[pattern.Length];

            bool IsPatternContainsScore = false;
            
            input_text = input_text.ToLower();
            

            word_count = CountWords(input_text);

            string[] final_words = new string[word_count];

            string[] words = input_text.Split();
            

            for(int i = 0; i < pattern.Length; i++ ){
                if(pattern[i] == '-'){
                    score_index[i] = true;
                    IsPatternContainsScore = true;
                }
                else if(pattern[i] == '*'){
                    star_index[i] = true;
                }
            }

            words = PunctuationRemover(words);
            if(IsPatternContainsScore){
                final_words = FoundScore(words, pattern, word_count);
            }
            for (int i = 0; i < word_count; i++)
            {
                System.Console.WriteLine(final_words[i]);
            }
            
        }   
    }
}

