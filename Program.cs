using System;

namespace word_search
{
    class Program
    {
        static string[] PunctuationRemover(string[] words){

            char[] punctuations = new char[]{'.', ',', '!', '?', '(', ')', '"', '-', ':', ';', '[', ']'};

            //Remove any punctuation, use an extra variable to escape outofindex errors
            for (int i = 0; i < words.Length; i++){
                bool exception_escape = false;

                for (int j = 0; j < words[i].Length; j++){

                    for (int k = 0; k < 12; k++){
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
            string[] fitting_words = new string[word_count];
            
            //Chec
            for (int i = 0; i < word_count; i++)
            {
                //Assign current word to another array if the current character is either matches with the pattern or is score(-) char
                bool flag = true;
                if(words[i].Length == pattern.Length){
                    for (int j = 0; j < pattern.Length; j++){
                        if(pattern[j] != '-' && pattern.ToLower()[j] != words[i].ToLower()[j]){
                            flag = false;
                        }

                        //Check if there is duplicate words
                        for (int k = 0; k < i; k++){
                            if (fitting_words[k] == words[i]){
                                flag = false;
                            }
                        }
                    }
                    if(flag){
                        
                        fitting_words[i] = words[i];
                    }
                }
            }

            return fitting_words;
        }

        static int[] StarIndexer(string pattern, int word_count){ //This function saves star chars positions in an array
            int[] starindex_temp = new int[word_count];
            int counter = 0;

            //Assign star position to an array
            for (int i = 0; i < pattern.Length; i++){
                if(pattern[i] == '*'){
                    //Assign a negative value if the 0 index possess a star character
                    if(i == 0){
                        starindex_temp[0] = -1;
                    }
                    else{
                        starindex_temp[counter] = i;
                        counter++;
                    }
                    
                }
            }

            //Count how many star positions are registired
            counter = 0;
            for (int i = 0; i < word_count; i++){
                    if(starindex_temp[i] != 0){
                        counter++;
                    } 
            }

            //Intruduce a new variable with correct size
            int[] starindex = new int[counter];

            //Fill new array with the values saved before
            for (int i = 0; i < word_count; i++){
                if(starindex_temp[i] != 0){
                    starindex[i] = starindex_temp[i];
                }
                else{
                    break;
                }
            }

            //Fıx a special event which happens when the first character of the pattern is a star
            if(starindex[0] == -1){
                starindex[0] = 0;
            }

            return starindex;

        }
        static string[] FoundStar(string[] words, string pattern, int word_count){
            string[] fitting_words = new string[word_count];
            int[] starindex = new int[word_count];
            string[] substring = new string[pattern.Length];

            starindex = StarIndexer(pattern, word_count);
            int counter = 0;
            //If statement that only executes if the pattern has one star at the beginning of the string
            if(pattern[0] == '*' && pattern.Length != 1 && starindex.Length == 1){
                string special_substring = pattern.Substring(1).ToLower();
                
                for (int i = 0; i < word_count; i++){
                    if(words[i].ToLower().IndexOf(special_substring) != - 1){
                        if(words[i].IndexOf(special_substring) == words[i].Length - special_substring.Length){
                            fitting_words[i] = words[i];
                        }  
                    }                     
                }
            }

            //If statement that only executes if the pattern has one star at the end of the string
            else if(pattern[pattern.Length - 1] == '*' && pattern.Length != 1 && starindex.Length == 1){
                string special_subtring = pattern.Substring(0,pattern.Length - 1).ToLower();

                for (int i = 0; i < word_count; i++){
                    if(words[i].ToLower().IndexOf(special_subtring) == 0){
                        fitting_words[i] = words[i];
                    }
                }
            }
            
            //If statement that only executes if the pattern is a single star string
            else if(pattern[0] == '*' && pattern.Length == 1){
                fitting_words = words;
            }

            //This part covers all the other cases
            else{

                //Saves the substring that start at the start of the string and ends at the first star
                substring[counter] = pattern.Substring(0, starindex[0]);
                counter++;

                //Saves the substrings that is stuck between stars
                for (int i = 0; i < starindex.Length - 1; i++){
                    substring[counter] = pattern.Substring(starindex[i]+1, (starindex[i+1] - starindex[i])-1);
                    counter ++;
                }            

                //Saves the substring that start at the the last star and ends at the end of the string
                substring[counter] = pattern.Substring((starindex[starindex.Length - 1])+1);
                counter++;

                //Assign words to an array if the word contains required substrings.
                for (int i = 0; i < word_count; i++){
                    bool flag = true;

                    for (int j = 0; j < counter; j++){
                        if(!words[i].ToLower().Contains(substring[j].ToLower()) && substring[j] != null){
                            flag = false;
                            break;
                        }

                        //Check the order of the substring and do not assign the word if the order is wrong
                        for (int k = 0; k < substring.Length - 1; k++){

                            if(substring[k] != null && substring[k+1] != null){
                                if(words[i].IndexOf(substring[k]) > words[i].IndexOf(substring[k + 1])){
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }

                    if(flag){
                        fitting_words[i] = words[i];
                    }
                }
            }

            //Remove duplicate words in the array
            for (int i = 0; i < fitting_words.Length; i++){
                
                for (int j = 0; j < fitting_words.Length; j++){

                    if(fitting_words[i] == fitting_words [j] && i != j){
                        fitting_words[i] = null;
                        break;
                    }
                }
            }            
            return fitting_words;
        }
        static void Main(string[] args){
            Console.Clear();

            string input_text;
            string pattern;
            int word_count;            

            //Take user inputs
            Console.Write("Please enter the input text: ");
            input_text = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Please enter the search pattern: ");
            pattern = Console.ReadLine();
            Console.WriteLine();
            
            //Split input text into a word array
            string[] words = input_text.Split();

            word_count = words.Length;

            //Check if the pattern consist star or score
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

            //Call PunctuationRemover and remove any punctuation characters
            words = PunctuationRemover(words);

            //Call the correct function according the IsPAtternContains variables
            if(IsPatternContainsScore){
                words = FoundScore(words, pattern, word_count);
            }
            else if(IsPatternContainsStar){
                words = FoundStar(words, pattern, word_count);
            }

            //Output the words while excluding null values
            for (int i = 0; i < word_count; i++)
            {
                if(words[i] != null){
                    System.Console.WriteLine(words[i]);
                }
            }
        }   
    }
}
