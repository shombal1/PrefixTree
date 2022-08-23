
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PrefixTree
{


    public class knot
    {
        public SortedDictionary<char, knot> Child { get; set; } = new SortedDictionary<char, knot>();
        public bool EndWord { get; set; } = false;
    }

    public class SortedPrefixTree
    {
        private knot root = new knot();
        public int Count { get; private set; } = 0;

        private bool RecursionAdd(in String word, int ind, knot Knot)
        {
            if (ind < word.Length)
            {
                try
                {
                    Knot.Child.Add(word[ind], new knot());
                    return RecursionAdd(word, ind + 1, Knot.Child[word[ind]]);
                }
                catch
                {
                    return RecursionAdd(word, ind + 1, Knot.Child[word[ind]]);
                }
            }
            else
            {
                if (Knot.EndWord)
                {
                    return false;
                }
                else
                {
                    Count++;
                    Knot.EndWord = true;
                    return true;
                }
            }
        }

        private bool RecursionRemove(in String word, int ind, knot Knot)
        {
            if (ind < word.Length)
            {
                try
                {
                    return RecursionRemove(word, ind + 1, Knot.Child[word[ind]]);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                if (Knot.EndWord)
                {
                    Knot.EndWord = false;
                    Count--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void RecursionMultiFind(in String word, knot Knot, List<string> strings)
        {
            if (Knot.EndWord == true)
            {
                strings.Add(word);
            }
            foreach (var a in Knot.Child)
            {
                RecursionMultiFind(word.Insert(word.Length, Convert.ToString(a.Key)), a.Value, strings);
            }
        }
        private void RecursionMultiFind(in String word, knot Knot, BindingList<string> strings)
        {
            if (Knot.EndWord == true)
            {
                strings.Add(word);
            }
            foreach (var a in Knot.Child)
            {
                RecursionMultiFind(word.Insert(word.Length, Convert.ToString(a.Key)), a.Value, strings);
            }
        }

        public bool Add(String word)
        {
            return RecursionAdd(word, 0, root);
        }

        public bool Find(String word)
        {
            knot LastKnot = root;
            for (int i = 0; i < word.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[word[i]];
                }
                catch
                {
                    return false;
                }
            }
            return LastKnot.EndWord;
        }

        public bool Remove(String word)
        {
            return RecursionRemove(word, 0, root);
        }

        public void Clear()
        {
            root.Child.Clear();
            Count = 0;
        }

        public List<string> SimilarWord(String BeginWord)
        {
            List<string> strings = new List<string>();
            knot LastKnot = root;

            for (int i = 0; i < BeginWord.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[BeginWord[i]];
                }
                catch
                {
                    return strings;
                }
            }
            RecursionMultiFind(BeginWord, LastKnot, strings);
            return strings;
        }
        public void SimilarWord(String BeginWord, BindingList<string> strings)
        {
            knot LastKnot = root;
            for (int i = 0; i < BeginWord.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[BeginWord[i]];
                }
                catch
                {
                }
            }
            RecursionMultiFind(BeginWord, LastKnot, strings);
        }
    }

}