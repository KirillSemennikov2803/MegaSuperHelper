using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace MegaSuperHelper
{
    class HelperSqlLite
    {
       private SQLiteConnection notes_db;
        public HelperSqlLite()
        {
           string dbPath = Path.Combine(
         System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
         "ormdemo.db3");
            this.notes_db = new SQLiteConnection(dbPath);
            this.notes_db.CreateTable<Notes>();
        }
        public bool create_new_notes(string text)
        {
            try
            {
                var new_notes = new Notes(text);
                this.notes_db.Insert(new_notes);
                return true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<string> create_list_of_notes()
        {
           var data = new List<string>();
           var table = this.notes_db.Table<Notes>();
            foreach (var s in table)
            {
                if (s.text.Length < 20)
                {
                    data.Add(s.text);
                }
                else
                {
                    data.Add(s.text.Substring(0, 20));
                }
            }
            data.Add("Добавить новый Элемент");
            return data; 
        }
        public SortedDictionary<long, string> get_notes_ui_list() {
            SortedDictionary<long, string> dict =
                new SortedDictionary<long, string>();

            var table = this.notes_db.Table<Notes>();
            long index = 0;
            foreach (var s in table)
            {
                dict.Add(index, s.guid);
                index++;
            }
            dict.Add(index,"add");
            return dict;
        }

        public void change_notes_text(string guid,string text)
        {
            try
            {
                var note = this.notes_db.Get<Notes>(guid);
                note.text = text;
                this.notes_db.InsertOrReplace(note);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }

        }
        public string return_notes_text_from_guid(string guid)
        {
            var note = this.notes_db.Get<Notes>(guid);
            return note.text;

        }

    }
}