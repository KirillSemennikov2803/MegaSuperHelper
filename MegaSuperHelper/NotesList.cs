using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MegaSuperHelper
{
    [Activity(Label = "NotesList")]
    public class NotesList : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var sql_lite = new HelperSqlLite();
            var list_notes =  sql_lite.create_list_of_notes();
            var dict_notes = sql_lite.get_notes_ui_list();
            this.ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, list_notes);
            this.ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent intent = new Intent(this, typeof(NotesView));
                List<string> guid = new List<string>();
                guid.Add(dict_notes[args.Id]);
                intent.PutStringArrayListExtra("guid", guid);
                StartActivity(intent);
            };
        }
    }
}