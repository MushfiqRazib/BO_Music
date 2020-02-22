using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class SearchKeyWordMap
    {

        private Hashtable _searchKeywordMaps = new Hashtable();




        public bool IsKeyExists(string key)
        {
            return this._searchKeywordMaps.ContainsKey(key);
        }

        public List<SearchKeyword> GetKeyWords(string key)
        {
            return this._searchKeywordMaps[key] as List<SearchKeyword>;

        }


        public void AddKeyWords(string key, List<SearchKeyword> value)
        {
            if (this._searchKeywordMaps.Count > 1000)
            {

                this._searchKeywordMaps.Clear();
            }
            this._searchKeywordMaps.Add(key, value);

        }

    }
}
