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
using SQLite;
namespace MegaSuperHelper
{
    [Table("Notes")]
    class Notes
    {
        [Column("text")]
        public string text{ get; set; }
        [PrimaryKey, Column("guid")]
        public string guid
        { get; set; }

        public Notes()
        {

        }
        public Notes(string text)
        {
            this.text = text;
            this.guid = GuidGenerator.generete_guid();
        }
    }
}