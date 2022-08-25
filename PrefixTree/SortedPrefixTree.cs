
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PrefixTree
{
    public struct pair<T1, T2>
    {
        public T1 first { get; set; }
        public T2 second { get; set; }
    }

    public class SortedPrefixTree<T> where T : class, new()
    {
        protected class SortedKnot
        {
            public virtual bool EndWord { get; set; } = false;
            public SortedDictionary<Char, SortedKnot> Child { get; set; } = new SortedDictionary<Char, SortedKnot>();
            public T Value { get; set; }
        }

        public int Count { get; private set; } = 0;
        private SortedKnot root = new SortedKnot();

        private bool RecursionAdd(in String word, int ind, SortedKnot Knot, T Value)
        {
            if (ind < word.Length)
            {
                try
                {
                    Knot.Child.Add(word[ind], new SortedKnot());
                    return RecursionAdd(word, ind + 1, Knot.Child[word[ind]], Value);
                }
                catch
                {
                    return RecursionAdd(word, ind + 1, Knot.Child[word[ind]], Value);
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
                    Knot.Value = Value;
                    Knot.EndWord = true;
                    return true;
                }
            }
        }

        private bool RecursionRemove(in String word, int ind, SortedKnot Knot)
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
                    Count--;
                    Knot.EndWord = false;
                    Knot.Value = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void RecursionMultiFind(in String word, SortedKnot Knot, List<pair<String, T>> strings)
        {
            if (Knot.EndWord == true)
            {
                strings.Add(new pair<String, T>() { first = word, second = Knot.Value });
            }
            foreach (var a in Knot.Child)
            {
                RecursionMultiFind(word.Insert(word.Length, Convert.ToString(a.Key)), a.Value, strings);
            }
        }

        public bool Add(String word, T Value)
        {
            return RecursionAdd(word, 0, root, Value);
        }

        public bool Remove(String word)
        {
            return RecursionRemove(word, 0, root);
        }

        public bool Find(String word, out T Value)
        {
            SortedKnot LastKnot = root;
            for (int i = 0; i < word.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[word[i]];
                }
                catch
                {
                    Value = null;
                    return false;
                }
            }
            if (LastKnot.EndWord)
            {
                Value = LastKnot.Value;
                return true;
            }
            Value = null;
            return false;
        }
        public pair<bool, T> Find(String word)
        {
            SortedKnot LastKnot = root;
            for (int i = 0; i < word.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[word[i]];
                }
                catch
                {
                    return new pair<bool, T>() { first = false, second = null };
                }
            }
            return new pair<bool, T>() { first = true, second = LastKnot.Value };
        }

        public List<pair<String, T>> SimilarWord(String BeginningWord)
        {
            List<pair<String, T>> strings = new List<pair<String, T>>();
            SortedKnot LastKnot = root;

            for (int i = 0; i < BeginningWord.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[BeginningWord[i]];
                }
                catch
                {
                    return strings;
                }
            }
            RecursionMultiFind(BeginningWord, LastKnot, strings);
            return strings;
        }

        public void Clear()
        {
            root.Child.Clear();
            Count = 0;
        }
    }

    public class SortedPrefixTree
    {
        protected class SortedKnot
        {
            public SortedDictionary<Char, SortedKnot> Child { get; set; } = new SortedDictionary<char, SortedKnot>();
            public bool EndWord { get; set; } = false;
        }

        private SortedKnot root = new SortedKnot();
        public int Count { get; private set; } = 0;


        private bool RecursionAdd(in String word, int ind, SortedKnot Knot)
        {
            if (ind < word.Length)
            {
                try
                {
                    Knot.Child.Add(word[ind], new SortedKnot());
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

        private bool RecursionRemove(in String word, int ind, SortedKnot Knot)
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

        private void RecursionMultiFind(in String word, SortedKnot Knot, List<string> strings)
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
        private void RecursionMultiFind(in String word, SortedKnot Knot, BindingList<string> strings)
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
            SortedKnot LastKnot = root;
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

        public List<string> SimilarWord(String BeginningWord)
        {
            List<string> strings = new List<string>();
            SortedKnot LastKnot = root;

            for (int i = 0; i < BeginningWord.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[BeginningWord[i]];
                }
                catch
                {
                    return strings;
                }
            }
            RecursionMultiFind(BeginningWord, LastKnot, strings);
            return strings;
        }
        public void SimilarWord(String BeginningWord, BindingList<string> strings)
        {
            SortedKnot LastKnot = root;
            for (int i = 0; i < BeginningWord.Length; i++)
            {
                try
                {
                    LastKnot = LastKnot.Child[BeginningWord[i]];
                }
                catch
                {
                }
            }
            RecursionMultiFind(BeginningWord, LastKnot, strings);
        }
    }

}