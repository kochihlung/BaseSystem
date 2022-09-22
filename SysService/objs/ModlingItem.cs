using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysService.objs
{
    public class ModlingData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ModlingSetup
    {
        public ModlingSetup()
        {
            Col = new List<ModlingSetupDtl>();
        }
        public string Name { get; set; }
        public string text { get; set; }
        public List<ModlingSetupDtl> Col { get; set; }
    }

    public class ModlingSetupDtl
    {
        public ModlingSetupDtl()
        {
            IsModling = false;
            Readonly = false;
            Required = false;
            width = 150;
            align = "center";
        }
        public string ColName { get; set; }
        public string text { get; set; }

        /// <summary>
        /// Number,Date,Time,Combobox,Textbox
        /// </summary>
        public string UI { get; set; }
        public bool IsModling { get; set; }
        public object Source { get; set; }
    
        public bool Readonly { get; set; }
        public bool Required { get; set; }
        public int width { get; set; }
        public string align { get; set; }
        public bool Show { get; set; }
        public int sort { get; set; }
    }

    public class MDSetup
    {
        public MDSetup()
        {
            group = "Settings";
            editor = new MD_editor();
        }
        public string text { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string group { get; set; }
        public MD_editor editor { get; set; }
    }
    public class MD_editor
    {
        public MD_editor()
        {

        }
        public MD_editor(string _type)
        {
            type = _type;
            options = new MD_editor_options();
        }
        public string type { get; set; }
        public MD_editor_options options { get; set; }
    }
    public class MD_editor_options
    {
        public MD_editor_options()
        {
            data = new List<ComboboxItem>();
        }
        public string on { get { return "1"; } }
        public string off { get { return "0"; } }

        public string valueField { get { return "text"; } }
        public string textField { get { return "text"; } }
        public List<ComboboxItem> data { get; set; }
        public string type { get; set; }
    }
}



