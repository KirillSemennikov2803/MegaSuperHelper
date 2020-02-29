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
    [Activity(Label = "NotesView")]
    public class NotesView : Activity
    {
        private bool its_new_notes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var guid = Intent.Extras.GetStringArrayList("guid")[0];
            SetContentView(Resource.Layout.note);
            if (guid == "add") {
                this.its_new_notes = true;
            }
            else
            {
                this.its_new_notes = false;
                var sql_lite = new HelperSqlLite();
                var text = sql_lite.return_notes_text_from_guid(guid);
                EditText text_edit = FindViewById<EditText>(Resource.Id.notes_text_view);
                text_edit.Text = text;
            }
            Button go_back = FindViewById<Button>(Resource.Id.go_back);
           

            go_back.Click += (sender, e) =>
            {
                close_form(guid);
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

               
        }
        private bool close_form(string guid)
        {
            try
            {
                EditText notes_text_view  = FindViewById<EditText>(Resource.Id.notes_text_view);
                var text = notes_text_view.Text;
                if (text == "")
                {
                    text = "Вы не написали текст но не перживайте программа сделала это за вас";
                }
                var sql_lite = new HelperSqlLite();
                if (this.its_new_notes)
                {
                    
                    sql_lite.create_new_notes(text);
                   
                }
                else
                {
                    sql_lite.change_notes_text(guid, text);
                }
                return true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
       
    }
    
}