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

                        for (int k = 0; k < i; k++)
                        {
                            if (elligable_words[k] == words[i]){
                                flag = false;
                            }
                        }
                    }
                    if(flag){
                        
                        elligable_words[i] = words[i];
                    }
                }
            }

            return elligable_words;
        }

        static int[] StarIndexer(string pattern, int word_count){
            int[] starindex_temp = new int[word_count];
            int counter = 0;

            for (int i = 0; i < pattern.Length; i++)
            {
                if(pattern[i] == '*'){
                    if(i == 0){
                        starindex_temp[0] = -1;
                    }
                    else{
                        starindex_temp[counter] = i;
                        counter++;
                    }
                    
                }
            }

            counter = 0;
            for (int i = 0; i < word_count; i++)
            {
                    if(starindex_temp[i] != 0){
                        counter++;
                    } 
            }

            int[] starindex = new int[counter];

            int searchstart = 0;
            for (int i = 0; i < counter; i++)
            {
                for (int j = searchstart; j < starindex_temp.Length; j++)
                {
                    if(starindex_temp[j] != 0){
                        starindex[i] = starindex_temp[j];
                        searchstart = j +1;
                    }
                }
            }

            if(starindex[0] == -1){
                starindex[0] = 0;
            }

            return starindex;

        }
        static string[] FoundStar(string[] words, string pattern, int word_count){
            string[] elligable_words = new string[word_count];
            int[] starindex = new int[word_count];
            string[] substring = new string[pattern.Length];

            starindex = StarIndexer(pattern, word_count);
            
            int counter = 0;
            for (int i = 0; i < starindex.Length - 1; i++){
                substring[counter] = pattern.Substring(starindex[i], (starindex[i+1] - starindex[i]));
                counter ++;
            }
            substring[counter] = pattern.Substring(0, starindex[0]);
            counter ++;

            substring[counter] = pattern.Substring((starindex[starindex.Length - 1])+1);
            counter++;

            for (int i = 0; i < word_count; i++)
            {
                bool flag = true;
                for (int j = 0; j < counter; j++)
                {
                    if(!words[i].ToLower().Contains(substring[j].ToLower()) && substring[j] != null){
                        flag = false;
                        break;
                    }
                }
                if(flag){
                    elligable_words[i] = words[i];
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
            pattern = "do*";           
            
            string[] words = input_text.Split();

            word_count = words.Length;

            bool IsPatternContainsScore = false;
            bool IsPatternContainsStar = false;
            for(int i = 0; i < pattern.Length; i++ ){
                if(pattern[i] == '-'){
                    IsPatternContainsScore = true;
                }
                else if(pattern[i] == '*'){
                    IsPatternContainsStar = true;
                }
            }

            words = PunctuationRemover(words);

            if(IsPatternContainsScore){
                words = FoundScore(words, pattern, word_count);
            }
            else if(IsPatternContainsStar){
                words = FoundStar(words, pattern, word_count);
            }
            for (int i = 0; i < word_count; i++)
            {
                if(words[i] != null){
                    System.Console.WriteLine(words[i]);
                }
            }
        }   
    }
}

//To-Do

