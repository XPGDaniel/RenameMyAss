using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using RenameMyAss.Class;
using System.Collections.Specialized;
using System.Diagnostics;

namespace RenameMyAss
{
    public partial class Form_Main : Form
    {
        private XMLHelper xmlH = new XMLHelper();
        const int ALT = 32;
        const int CTRL = 8;
        const int SHIFT = 4;
        const string WinRAR = @"C:\Program Files\WinRAR\WinRAR.exe";
        public bool MakeRAR = false;
        public Form_Main()
        {
            InitializeComponent();
            comboBox_Target.SelectedIndex = 0;
            comboBox_Mode.SelectedIndex = 3;
            button_Undo.Enabled = false;
            button_UpdateRule.Enabled = false;
            button_MoveUp.Enabled = false;
            button_MoveDown.Enabled = false;
            button_RemoveRule.Enabled = false;
            button_RemovefromList.Enabled = false;
            button_Preview.Enabled = false;
            button_Rename.Enabled = false;
            AuxParameters_Control(false);
            if(File.Exists(WinRAR)) comboBox_Target.Items.Add("FolderName");
        }
        private enum MoveDirection { Up = -1, Down = 1 };
        #region ComboBoxes
        private void comboBox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Mode.Items.Clear();
            AuxParameters_Control(false);
            label_Parameter2.Text = "Parameter2";
            switch (comboBox_Target.Text.ToLowerInvariant())
            {
                case "filename":
                case " fileextension":
                case "foldername":
                    comboBox_Mode.Items.Add("Prefix");
                    comboBox_Mode.Items.Add("Suffix");
                    comboBox_Mode.Items.Add("Remove");
                    comboBox_Mode.Items.Add("Replace");
                    comboBox_Mode.Items.Add("UPPERCASE");
                    comboBox_Mode.Items.Add("lowercase");
                    comboBox_Mode.Items.Add("Insert");
                    if (comboBox_Target.Text.ToLowerInvariant() == "foldername") comboBox_Mode.Items.Add("MakeRAR");
                    comboBox_Mode.Visible = true;
                    label_Mode.Visible = true;
                    comboBox_Parameter1.Visible = true;
                    label_Parameter1.Visible = true;
                    comboBox_Parameter2.Visible = true;
                    label_Parameter2.Visible = true;
                    break;
                //case "rarfolder":
                //    comboBox_Mode.Text = "";
                //    comboBox_Mode.Visible = false;
                //    label_Mode.Visible = false;
                //    comboBox_Parameter1.Visible = false;
                //    label_Parameter1.Visible = false;
                //    comboBox_Parameter2.Visible = false;
                //    label_Parameter2.Visible = false;
                //    break;
            }
        }

        private void comboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Parameter1.Items.Clear();
            AuxParameters_Control(false);
            label_Parameter2.Text = "Parameter2";
            switch (comboBox_Mode.Text.ToLowerInvariant())
            {
                case "prefix":
                case "suffix":
                    label_Parameter1.Text = "Target phase";
                    comboBox_Parameter1.Text = "";
                    comboBox_Parameter2_Controls(false, null);
                    break;
                case "replace":
                    label_Parameter1.Text = "Target phase";
                    comboBox_Parameter1.Text = "";
                    comboBox_Parameter2_Controls(true, "Replace with");
                    break;
                case "remove":
                case "uppercase":
                case "lowercase":
                    label_Parameter1.Text = "Pattern";
                    comboBox_Parameter1.Text = "";
                    comboBox_Parameter2_Controls(false, null);
                    comboBox_Parameter1.Items.Add("All");
                    comboBox_Parameter1.Items.Add("Left");
                    comboBox_Parameter1.Items.Add("Right");
                    if (comboBox_Mode.Text.ToLowerInvariant() == "remove")
                    {
                        comboBox_Parameter1.Items.Add("Center");
                    }
                    comboBox_Parameter1.Items.Add("FirstLetter");
                    comboBox_Parameter1.Items.Add("MatchPhase");
                    if (comboBox_Mode.Text.ToLowerInvariant() == "remove")
                    {
                        comboBox_Parameter1.Items.Add("(*)");
                        comboBox_Parameter1.Items.Add("(*");
                        comboBox_Parameter1.Items.Add("*)");
                    }
                    break;
                case "insert":
                    label_Parameter1.Text = "Pattern";
                    comboBox_Parameter1.Text = "";
                    comboBox_Parameter2_Controls(false, null);
                    comboBox_Parameter1.Items.Add("Phase");
                    comboBox_Parameter1.Items.Add("Digit");
                    break;
                case "makerar":
                    label_Parameter1.Text = "Parameter1";
                    comboBox_Parameter1.Text = "";
                    comboBox_Parameter2_Controls(false, null);
                    break;
                //default:
                //comboBox_Parameter1.Items.Clear();
                //break;
            }
        }
        private void comboBox_Parameter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AuxParameters_Control(false);
            switch (comboBox_Mode.Text.ToLowerInvariant())
            {
                case "remove":
                case "uppercase":
                case "lowercase":
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "(*)":
                        case "(*":
                        case "*)":
                        case "firstletter":
                        case "all":
                            comboBox_Parameter2_Controls(false, null);
                            break;
                        case "left":
                        case "right":
                            comboBox_Parameter2_Controls(true, "Length");
                            break;
                        case "center":
                            comboBox_Parameter2_Controls(true, "Range");
                            break;
                        case "matchphase":
                            comboBox_Parameter2_Controls(true, "Target phase");
                            break;
                        default:
                            comboBox_Parameter2_Controls(false, null);
                            break;
                    }
                    break;
                case "insert":
                    comboBox_Parameter2_Controls(true, "Position");
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "phase":
                            AuxParameters_Control(true, "Phase");
                            break;
                        case "digit":
                            AuxParameters_Control(true, "Length", "Start from", "Step");
                            break;
                    }
                    break;
            }
        }
        private void comboBox_Parameter2_Controls(bool Visibility, string label)
        {
            comboBox_Parameter2.Enabled = Visibility;
            label_Parameter2.Visible = Visibility;
            label_Parameter2.Text = !string.IsNullOrEmpty(label) ? label : "";
            comboBox_Parameter2.Text = (label_Parameter2.Text == "Position" || label_Parameter2.Text == "Length") ? "0" : "";
            comboBox_Parameter2.Visible = Visibility;
        }
        private void AuxParameters_Control(bool Visibility, string Ax1 = null, string Ax2 = null, string Ax3 = null)
        {
            label_AuxParameter1.Visible = Visibility;
            label_AuxParameter1.Text = !string.IsNullOrEmpty(Ax1) ? Ax1 : "";
            comboBox_AuxParameter1.Text = "";
            comboBox_AuxParameter1.Visible = Visibility;
            comboBox_AuxParameter1.Enabled = Visibility;

            if (string.IsNullOrEmpty(Ax2))
            {
                Visibility = false;
            }
            label_AuxParameter2.Visible = Visibility;
            label_AuxParameter2.Text = !string.IsNullOrEmpty(Ax2) ? Ax2 : "";
            comboBox_AuxParameter2.Text = "";
            comboBox_AuxParameter2.Visible = Visibility;
            comboBox_AuxParameter2.Enabled = Visibility;

            if (string.IsNullOrEmpty(Ax3))
            {
                Visibility = false;
            }
            label_AuxParameter3.Visible = Visibility;
            label_AuxParameter3.Text = !string.IsNullOrEmpty(Ax3) ? Ax3 : "";
            comboBox_AuxParameter3.Text = "";
            comboBox_AuxParameter3.Visible = Visibility;
            comboBox_AuxParameter3.Enabled = Visibility;
        }
        #endregion

        private void button_Preview_Click(object sender, EventArgs e)
        {
            if (listView_Rules.Items.Count > 0 && listView_FileList.Items.Count > 0)
            {
                List<FileToRename> newFilenameList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    newFilenameList.Add(ftr);
                }
                if (newFilenameList.Any())
                {
                    foreach (ListViewItem rule in listView_Rules.Items)
                    {
                        switch (rule.Text.ToLowerInvariant())
                        {
                            case "filename":
                                switch (rule.SubItems[1].Text.ToLowerInvariant())
                                {
                                    case "remove":
                                        newFilenameList = RuleHandler.RemoveFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "replace":
                                        newFilenameList = RuleHandler.ReplaceFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "prefix":
                                        newFilenameList = RuleHandler.PrefixFileName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "suffix":
                                        newFilenameList = RuleHandler.SuffixFileName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "uppercase":
                                        newFilenameList = RuleHandler.ChangeFileNameCase(newFilenameList, "up", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "lowercase":
                                        newFilenameList = RuleHandler.ChangeFileNameCase(newFilenameList, "down", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "insert":
                                        newFilenameList = RuleHandler.InsertIntoFileName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                }
                                break;
                            case "fileextension":
                                switch (rule.SubItems[1].Text.ToLowerInvariant())
                                {
                                    case "remove":
                                        newFilenameList = RuleHandler.RemoveFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "replace":
                                        newFilenameList = RuleHandler.ReplaceFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "prefix":
                                        newFilenameList = RuleHandler.PrefixFileExtension(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "suffix":
                                        newFilenameList = RuleHandler.SuffixFileExtension(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "uppercase":
                                        newFilenameList = RuleHandler.ChangeFileExtensionCase(newFilenameList, "up", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "lowercase":
                                        newFilenameList = RuleHandler.ChangeFileExtensionCase(newFilenameList, "down", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "insert":
                                        newFilenameList = RuleHandler.InsertIntoFileExtension(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                }
                                break;
                            case "foldername":
                                switch (rule.SubItems[1].Text.ToLowerInvariant())
                                {
                                    case "remove":
                                        newFilenameList = RuleHandler.RemoveFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "replace":
                                        newFilenameList = RuleHandler.ReplaceFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "prefix":
                                        newFilenameList = RuleHandler.PrefixFolderName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "suffix":
                                        newFilenameList = RuleHandler.SuffixFolderName(newFilenameList, rule.SubItems[2].Text);
                                        break;
                                    case "uppercase":
                                        newFilenameList = RuleHandler.ChangeFolderNameCase(newFilenameList, "up", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "lowercase":
                                        newFilenameList = RuleHandler.ChangeFolderNameCase(newFilenameList, "down", rule.SubItems[2].Text, rule.SubItems[3].Text);
                                        break;
                                    case "insert":
                                        newFilenameList = RuleHandler.InsertIntoFolderName(newFilenameList, rule.SubItems[2].Text, rule.SubItems[3].Text, rule.SubItems[4].Text);
                                        break;
                                    case "makerar":
                                        MakeRAR = true;
                                        break;
                                }
                                break;
                        }
                    }
                    for (int i = 0; i < newFilenameList.Count; i++)
                    {
                        ListViewItem fooItem = listView_FileList.Items.Find(newFilenameList[i].Path, false)[0];
                        fooItem.SubItems[2].Text = newFilenameList[i].FileName;
                    }
                }
                newFilenameList.Clear();
            }
        }
        private void button_Rename_Click(object sender, EventArgs e)
        {
            button_Preview_Click(null, null);
            if (listView_Rules.Items.Count > 0 && listView_FileList.Items.Count > 0)
            {
                List<FileToRename> RenameList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    ftr.NewFileName = vi.SubItems[2].Text;
                    RenameList.Add(ftr);
                }
                if (RenameList.Any())
                {

                    switch (comboBox_Target.Text.ToLowerInvariant())
                    {
                        case "foldername":
                            foreach (FileToRename fileCandidate in RenameList)
                            {
                                if (Directory.Exists(fileCandidate.Path))
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName))
                                        {
                                            if (fileCandidate.Path != Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName)
                                            {
                                                Directory.Move(fileCandidate.Path, Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName);
                                                fileCandidate.Result = "Rename OK";
                                            }
                                            if (MakeRAR)
                                            {//"C:\Program Files\WinRAR\WinRAR.exe" a -ep "C:\Users\Hüseyin\Desktop\deneme.rar" "C:\Users\Hüseyin\Desktop\AA\"
                                                using (Process p = new Process())
                                                {
                                                    p.StartInfo.FileName = "cmd.exe";
                                                    p.StartInfo.FileName = WinRAR;
                                                    p.StartInfo.RedirectStandardOutput = true;
                                                    p.StartInfo.RedirectStandardError = true;
                                                    p.StartInfo.UseShellExecute = false;
                                                    p.StartInfo.CreateNoWindow = false; //Default:true
                                                    //p.StartInfo.Arguments = "/c \"\"" + WinRAR + "\" a -ep1 -r \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + ".rar\" \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + "\\*\" -rr3p\"";
                                                    p.StartInfo.Arguments = " a -ep1 -r \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + ".rar\" \"" + Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName + "\\*\" -rr3p";
                                                    p.Start();
                                                    //string stdoutx = p.StandardOutput.ReadToEnd();
                                                    //string stderrx = p.StandardError.ReadToEnd();
                                                    p.WaitForExit();
                                                    //Console.WriteLine("Exit code : {0}", p.ExitCode);
                                                    //Console.WriteLine("Stdout : {0}", stdoutx);
                                                    //Console.WriteLine("Stderr : {0}", stderrx);
                                                    fileCandidate.Result += "RAR OK";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            fileCandidate.Result = "Invalid new FolderName";
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = "Rename Fail";
                                        //throw;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (FileToRename fileCandidate in RenameList)
                            {
                                if (File.Exists(fileCandidate.Path))
                                {
                                    try
                                    {
                                        FileInfo fi = new FileInfo(fileCandidate.Path);
                                        if (!string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(fileCandidate.NewFileName)) && !string.IsNullOrEmpty(Path.GetExtension(fileCandidate.NewFileName)))
                                        {
                                            File.Move(fi.FullName, fi.FullName.Replace(fileCandidate.FileName, fileCandidate.NewFileName));
                                            fileCandidate.Result = "Rename OK";
                                        }
                                        else
                                        {
                                            fileCandidate.Result = "Invalid new Filename or Extension";
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = "Rename Fail";
                                        //throw;
                                    }
                                }
                            }
                            break;
                    }
                }
                for (int i = 0; i < RenameList.Count; i++)
                {
                    ListViewItem fooItem = listView_FileList.Items.Find(RenameList[i].Path, false)[0];
                    fooItem.SubItems[3].Text = RenameList[i].Result;
                }
                RenameList.Clear();
                button_Undo.Enabled = true;
            }
            MessageBox.Show("Done!");
            MakeRAR = false;
        }
        private void button_Undo_Click(object sender, EventArgs e)
        {
            if (listView_FileList.Items.Count > 0)
            {
                List<FileToRename> UndoList = new List<FileToRename>();
                foreach (ListViewItem vi in listView_FileList.Items)
                {
                    FileToRename ftr = new FileToRename();
                    ftr.Path = vi.Text;
                    ftr.FileName = vi.SubItems[1].Text;
                    ftr.NewFileName = vi.SubItems[2].Text;
                    ftr.Result = vi.SubItems[3].Text;
                    UndoList.Add(ftr);
                }
                if (UndoList.Any())
                {
                    switch (comboBox_Target.Text.ToLowerInvariant())
                    {
                        case "foldername":
                            foreach (FileToRename fileCandidate in UndoList)
                            {
                                string CurrentNewFolderName = Directory.GetParent(fileCandidate.Path).FullName + "\\" + fileCandidate.NewFileName;
                                if (Directory.Exists(CurrentNewFolderName))
                                {
                                    try
                                    {
                                        //FileInfo fi = new FileInfo(CurrentNewFileName);
                                        Directory.Move(CurrentNewFolderName, fileCandidate.Path);
                                        fileCandidate.Result = "Undo OK";
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = "Undo Fail";
                                        //throw;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (FileToRename fileCandidate in UndoList)
                            {
                                string CurrentNewFileName = fileCandidate.Path.Replace(fileCandidate.FileName, fileCandidate.NewFileName);
                                if (File.Exists(CurrentNewFileName))
                                {
                                    try
                                    {
                                        FileInfo fi = new FileInfo(CurrentNewFileName);
                                        File.Move(fi.FullName, fi.FullName.Replace(fileCandidate.NewFileName, fileCandidate.FileName));
                                        fileCandidate.Result = "Undo OK";
                                    }
                                    catch (Exception)
                                    {
                                        fileCandidate.Result = "Undo Fail";
                                        //throw;
                                    }
                                }
                            }
                            break;
                    }
                    for (int i = 0; i < UndoList.Count; i++)
                    {
                        ListViewItem fooItem = listView_FileList.Items.Find(UndoList[i].Path, false)[0];
                        fooItem.SubItems[3].Text = UndoList[i].Result;
                    }
                }
                UndoList.Clear();
                button_Undo.Enabled = false;
            }
        }

        ///// <summary>
        ///// Routine to refresh the display
        ///// </summary>
        //private void RefreshView()
        //{
        //    //listView_FileList.Items.Clear();
        //    //string[] files = Directory.GetFiles(homeFolder);
        //    //foreach (string file in files)
        //    //{
        //    //    listView_FileList.Items.Add(file);
        //    //}
        //}

        /// <summary>
        /// Routine to get the current selection from the listview
        /// </summary>
        /// <returns>Seletced items or null if no selection</returns>
        //private string[] GetSelection()
        //{
        //    if (listView_FileList.SelectedItems.Count == 0)
        //        return null;

        //    string[] files = new string[listView_FileList.SelectedItems.Count];
        //    int i = 0;
        //    foreach (ListViewItem item in listView_FileList.SelectedItems)
        //    {
        //        files[i++] = item.Text;
        //    }
        //    return files;
        //}

        #region listView_FileList
        private void listView_FileList_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Directory.Exists(s))
                {
                    //Add files from folder
                    switch (comboBox_Target.Text.ToLowerInvariant())
                    {
                        case "foldername":
                            ListViewItem lvid = new ListViewItem();
                            DirectoryInfo di = new DirectoryInfo(s);
                            lvid.Name = di.FullName;
                            lvid.Text = s;
                            lvid.SubItems.Add(di.Name);
                            lvid.SubItems.Add("");
                            lvid.SubItems.Add("");
                            listView_FileList.Items.Add(lvid);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvid, lvid.Index));
                            break;
                        default:
                            List<string> filepaths = new List<string>();
                            filepaths.AddRange(Directory.GetFiles(s, "*.*", SearchOption.AllDirectories));
                            foreach (string item in filepaths)
                            {
                                ListViewItem lvi = new ListViewItem();
                                FileInfo fi = new FileInfo(item);
                                lvi.Name = fi.FullName;
                                lvi.Text = item;
                                lvi.SubItems.Add(fi.Name);
                                lvi.SubItems.Add("");
                                lvi.SubItems.Add("");
                                listView_FileList.Items.Add(lvi);
                                listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                            }
                            break;
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists(s))
                        {
                            ListViewItem lvi = new ListViewItem();
                            FileInfo fi = new FileInfo(s);
                            lvi.Name = fi.FullName;
                            lvi.Text = s;
                            lvi.SubItems.Add(fi.Name);
                            lvi.SubItems.Add("");
                            lvi.SubItems.Add("");
                            listView_FileList.Items.Add(lvi);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, "Failed to perform the" +
                          " specified operation:\n\n" + ex.Message,
                          "File operation failed", MessageBoxButtons.OK,
                          MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void listView_FileList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void listView_FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_FileList.SelectedItems.Count > 0)
            {
                button_RemovefromList.Enabled = true;
            }
            else
            {
                button_RemovefromList.Enabled = false;
            }
        }

        private void listView_FileList_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Set the effect based upon the KeyState.
            if ((e.KeyState & SHIFT) == SHIFT &&
                (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;

            }
            else if ((e.KeyState & CTRL) == CTRL &&
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.
                e.Effect = DragDropEffects.Move;

                // Implement the rather strange behaviour of explorer that if the disk
                // is different, then default to a COPY operation
                switch (comboBox_Target.Text.ToLowerInvariant())
                {
                    case "foldername":
                        string[] folders = (string[])e.Data.GetData(DataFormats.FileDrop);
                        if (folders.Length > 0 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            e.Effect = DragDropEffects.Copy;
                        break;
                    default:
                        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                        //if (files.Length > 0 && !files[0].ToUpper().StartsWith(homeDisk) &&
                        //    // Probably better ways to do this
                        //(e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                        if (files.Length > 0 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            e.Effect = DragDropEffects.Copy;
                        break;
                }
            }
            else
                e.Effect = DragDropEffects.None;

            // This is an example of how to get the item under the mouse
            //Point pt = listView_FileList.PointToClient(new Point(e.X, e.Y));
            //ListViewItem itemUnder = listView_FileList.GetItemAt(pt.X, pt.Y);
        }

        private void button_ResetList_Click(object sender, EventArgs e)
        {
            listView_FileList.Items.Clear();
            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //MakeRAR = false;
            button_RemovefromList.Enabled = false;
        }

        private void button_RemovefromList_Click(object sender, EventArgs e)
        {
            if (listView_FileList.SelectedItems.Count != 0)
            {

                string[] files = new string[listView_FileList.SelectedItems.Count];
                foreach (ListViewItem item in listView_FileList.SelectedItems)
                {
                    listView_FileList.Items.Remove(item);
                }
            }
        }
        #endregion


        #region listView_Rules
        private void listView_Rules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 0)
            {
                button_UpdateRule.Enabled = false;
                button_MoveUp.Enabled = false;
                button_MoveDown.Enabled = false;
                button_RemoveRule.Enabled = false;
                return;
            }

            if (listView_Rules.SelectedItems.Count == 1)
            {
                comboBox_Target.Text = listView_Rules.SelectedItems[0].Text;
                comboBox_Mode.Text = listView_Rules.SelectedItems[0].SubItems[1].Text;
                comboBox_Parameter1.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[2].Text) ? "" : listView_Rules.SelectedItems[0].SubItems[2].Text;
                comboBox_Parameter2.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[3].Text) ? "" : listView_Rules.SelectedItems[0].SubItems[3].Text;
                button_UpdateRule.Enabled = true;
                button_MoveUp.Enabled = true;
                button_MoveDown.Enabled = true;
                button_RemoveRule.Enabled = true;
                if (comboBox_Mode.Text.ToLowerInvariant() == "insert")
                {
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "phase":
                            comboBox_AuxParameter1.Text = string.IsNullOrEmpty(listView_Rules.SelectedItems[0].SubItems[4].Text) ? "" : listView_Rules.SelectedItems[0].SubItems[4].Text; ;
                            break;
                        case "digit":
                            string[] auxsplit = listView_Rules.SelectedItems[0].SubItems[4].Text.Split(';');
                            comboBox_AuxParameter1.Text = auxsplit[0];
                            comboBox_AuxParameter2.Text = auxsplit[1];
                            comboBox_AuxParameter3.Text = auxsplit[2];
                            break;
                    }
                }
            }

        }
        private void button_AddRule_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(comboBox_Target.Text) && !string.IsNullOrEmpty(comboBox_Mode.Text) && !string.IsNullOrEmpty(comboBox_Parameter1.Text)) ||
            (comboBox_Target.Text.ToLowerInvariant() == "foldername" && comboBox_Mode.Text.ToLowerInvariant() == "makerar"))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = comboBox_Target.SelectedItem.ToString();
                lvi.SubItems.Add(comboBox_Mode.Text);
                if (!string.IsNullOrEmpty(comboBox_Parameter1.Text))
                {
                    lvi.SubItems.Add(comboBox_Parameter1.Text);
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                if (!string.IsNullOrEmpty(comboBox_Parameter2.Text))
                {
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "left":
                            try
                            {
                                Convert.ToInt32(comboBox_Parameter2.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Numbers only !");
                                comboBox_Parameter2.Text = "0";
                            }
                            break;
                        case "right":
                            try
                            {
                                Convert.ToInt32(comboBox_Parameter2.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Numbers only !");
                                comboBox_Parameter2.Text = "0";
                            }
                            break;
                        default:
                            break;
                    }
                    lvi.SubItems.Add(comboBox_Parameter2.Text);
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                if (comboBox_Mode.Text.ToLowerInvariant() == "insert")
                {
                    string aux = "";
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "phase":
                            aux = comboBox_AuxParameter1.Text;
                            break;
                        case "digit":
                            if (!string.IsNullOrEmpty(comboBox_AuxParameter1.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter2.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter3.Text))
                                aux = string.Join(";", new string[] { comboBox_AuxParameter1.Text, comboBox_AuxParameter2.Text, comboBox_AuxParameter3.Text });
                            break;
                    }
                    if (!string.IsNullOrEmpty(aux)) lvi.SubItems.Add(aux);
                    comboBox_AuxParameter1.Text = "";
                    comboBox_AuxParameter2.Text = "";
                    comboBox_AuxParameter3.Text = "";
                }
                listView_Rules.Items.Add(lvi);
                comboBox_Parameter1.Text = (comboBox_Mode.Text.ToLowerInvariant() == "insert") ? comboBox_Parameter1.Text : "";
                comboBox_Parameter2.Text = "";
                listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
            }
        }
        private void button_UpdateRule_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1 &&
                !string.IsNullOrEmpty(comboBox_Target.Text) && !string.IsNullOrEmpty(comboBox_Mode.Text) &&
                !string.IsNullOrEmpty(comboBox_Parameter1.Text))
            {
                listView_Rules.SelectedItems[0].Text = comboBox_Target.Text;
                listView_Rules.SelectedItems[0].SubItems[1].Text = comboBox_Mode.Text;
                listView_Rules.SelectedItems[0].SubItems[2].Text = comboBox_Parameter1.Text;
                listView_Rules.SelectedItems[0].SubItems[3].Text = comboBox_Parameter2.Text;
                button_UpdateRule.Enabled = false;
                if (comboBox_Mode.Text.ToLowerInvariant() == "insert")
                {
                    switch (comboBox_Parameter1.Text.ToLowerInvariant())
                    {
                        case "phase":
                            listView_Rules.SelectedItems[0].SubItems[4].Text = comboBox_AuxParameter1.Text;
                            break;
                        case "digit":
                            if (!string.IsNullOrEmpty(comboBox_AuxParameter1.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter2.Text) && !string.IsNullOrEmpty(comboBox_AuxParameter3.Text))
                                listView_Rules.SelectedItems[0].SubItems[4].Text = string.Join(";", new string[] { comboBox_AuxParameter1.Text, comboBox_AuxParameter2.Text, comboBox_AuxParameter3.Text });
                            break;
                    }
                }
            }
        }

        private void button_MoveUp_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1)
            {
                int seletedIndex = listView_Rules.SelectedIndices[0];
                MoveItems(listView_Rules, MoveDirection.Up);
                if (seletedIndex > 0)
                {
                    listView_Rules.Items[seletedIndex - 1].Selected = true;
                }
                else
                {
                    listView_Rules.Items[seletedIndex].Selected = true;
                }
                listView_Rules.Select();
            }
        }

        private void button_MoveDown_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count == 1)
            {
                int seletedIndex = listView_Rules.SelectedIndices[0];
                MoveItems(listView_Rules, MoveDirection.Down);
                if (seletedIndex > 0 && seletedIndex < listView_Rules.Items.Count - 1)
                {
                    listView_Rules.Items[seletedIndex + 1].Selected = true;
                }
                else
                {
                    listView_Rules.Items[seletedIndex].Selected = true;
                }
                listView_Rules.Select();
            }
        }
        private void button_ResetRules_Click(object sender, EventArgs e)
        {
            listView_Rules.Items.Clear();
            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            MakeRAR = false;
        }

        private void button_RemoveRule_Click(object sender, EventArgs e)
        {
            if (listView_Rules.SelectedItems.Count != 0)
            {

                string[] files = new string[listView_Rules.SelectedItems.Count];
                foreach (ListViewItem item in listView_Rules.SelectedItems)
                {
                    if (item.SubItems[1].Text.ToLowerInvariant() == "makerar") MakeRAR = false;
                    listView_Rules.Items.Remove(item);
                }
                if (listView_Rules.Items.Count == 0)
                {
                    listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }
        private void button_SaveProfile_Click(object sender, EventArgs e)
        {
            if (listView_Rules.Items.Count > 0)
            {
                List<ListViewItem> Itemlist = new List<ListViewItem>();
                foreach (ListViewItem vi in listView_Rules.Items)
                {
                    Itemlist.Add(vi);
                }
                if (Itemlist.Any())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XML-File | *.xml";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        xmlH.SaveProfileXML(Itemlist, saveFileDialog.FileName);
                    }
                }
            }
        }
        private void button_LoadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName),
                                   ".xml",
                                   StringComparison.OrdinalIgnoreCase))
                {
                    // Invalid file type selected; display an error.
                    MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                                    "Invalid File Type",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // Optionally, force the user to select another file.
                    // ...
                }
                else
                {
                    List<Rules> rulist = xmlH.LoadProfileXML(ofd.FileName);
                    if (rulist.Any())
                    {
                        listView_Rules.Items.Clear();
                        foreach (Rules ru in rulist)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = ru.Target;
                            lvi.SubItems.Add(ru.Mode);
                            lvi.SubItems.Add(ru.Parameter1);
                            lvi.SubItems.Add(ru.Parameter2);
                            lvi.SubItems.Add(ru.AuxParameter);
                            listView_Rules.Items.Add(lvi);
                            listView_CollectionChanged(listView_Rules, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, lvi, lvi.Index));
                        }
                    }
                }
            }
        }
        private void listView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //The projects ListView has been changed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //MessageBox.Show("An Item Has Been Added To The ListView!");
                    if (listView_FileList.Items.Count > 0)
                    {
                        button_Preview.Enabled = true;
                        button_Rename.Enabled = true;
                    }
                    if (listView_Rules.Items.Count > 0)
                    {
                        button_SaveProfile.Enabled = true;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    //MessageBox.Show("The ListView Has Been Cleared!");
                    if (listView_FileList.Items.Count == 0)
                    {
                        button_Preview.Enabled = false;
                        button_Rename.Enabled = false;
                    }
                    if (listView_Rules.Items.Count == 0)
                    {
                        button_SaveProfile.Enabled = false;
                    }
                    break;
            }
        }
        private void MoveItems(ListView sender, MoveDirection direction)
        {
            bool valid = sender.SelectedItems.Count > 0 &&
                        ((direction == MoveDirection.Down && (sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1))
                        || (direction == MoveDirection.Up && (sender.SelectedItems[0].Index > 0)));

            if (valid)
            {
                bool start = true;
                int first_idx = 0;
                List<ListViewItem> items = new List<ListViewItem>();

                // ambil data
                foreach (ListViewItem i in sender.SelectedItems)
                {
                    if (start)
                    {
                        first_idx = i.Index;
                        start = false;
                    }
                    items.Add(i);
                }

                sender.BeginUpdate();

                // hapus
                foreach (ListViewItem i in sender.SelectedItems) i.Remove();

                // insert
                if (direction == MoveDirection.Up)
                {
                    int insert_to = first_idx - 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                else
                {
                    int insert_to = first_idx + 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                sender.EndUpdate();
            }
        }
        #endregion

    }
}
