namespace RenameMyAss
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.listView_FileList = new System.Windows.Forms.ListView();
            this.ch_Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_Before = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_After = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_Result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_ResetList = new System.Windows.Forms.Button();
            this.button_RemovefromList = new System.Windows.Forms.Button();
            this.listView_Rules = new System.Windows.Forms.ListView();
            this.ch_Target = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_Mode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_pa1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_pa2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_auxp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_MoveUp = new System.Windows.Forms.Button();
            this.button_MoveDown = new System.Windows.Forms.Button();
            this.button_RemoveRule = new System.Windows.Forms.Button();
            this.button_SaveProfile = new System.Windows.Forms.Button();
            this.button_AddRule = new System.Windows.Forms.Button();
            this.button_UpdateRule = new System.Windows.Forms.Button();
            this.label_Target = new System.Windows.Forms.Label();
            this.label_Mode = new System.Windows.Forms.Label();
            this.label_Parameter1 = new System.Windows.Forms.Label();
            this.label_Parameter2 = new System.Windows.Forms.Label();
            this.comboBox_Target = new System.Windows.Forms.ComboBox();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.comboBox_Parameter1 = new System.Windows.Forms.ComboBox();
            this.comboBox_Parameter2 = new System.Windows.Forms.ComboBox();
            this.button_Preview = new System.Windows.Forms.Button();
            this.button_Rename = new System.Windows.Forms.Button();
            this.button_ResetRules = new System.Windows.Forms.Button();
            this.button_Undo = new System.Windows.Forms.Button();
            this.label_AuxParameter1 = new System.Windows.Forms.Label();
            this.label_AuxParameter2 = new System.Windows.Forms.Label();
            this.comboBox_AuxParameter1 = new System.Windows.Forms.ComboBox();
            this.comboBox_AuxParameter2 = new System.Windows.Forms.ComboBox();
            this.label_AuxParameter3 = new System.Windows.Forms.Label();
            this.comboBox_AuxParameter3 = new System.Windows.Forms.ComboBox();
            this.button_LoadProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_FileList
            // 
            this.listView_FileList.AllowColumnReorder = true;
            this.listView_FileList.AllowDrop = true;
            this.listView_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_Path,
            this.ch_Before,
            this.ch_After,
            this.ch_Result});
            this.listView_FileList.FullRowSelect = true;
            this.listView_FileList.GridLines = true;
            this.listView_FileList.HideSelection = false;
            this.listView_FileList.Location = new System.Drawing.Point(12, 361);
            this.listView_FileList.Name = "listView_FileList";
            this.listView_FileList.Size = new System.Drawing.Size(984, 189);
            this.listView_FileList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_FileList.TabIndex = 0;
            this.listView_FileList.UseCompatibleStateImageBehavior = false;
            this.listView_FileList.View = System.Windows.Forms.View.Details;
            this.listView_FileList.SelectedIndexChanged += new System.EventHandler(this.listView_FileList_SelectedIndexChanged);
            this.listView_FileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_FileList_DragDrop);
            this.listView_FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_FileList_DragEnter);
            this.listView_FileList.DragOver += new System.Windows.Forms.DragEventHandler(this.listView_FileList_DragOver);
            // 
            // ch_Path
            // 
            this.ch_Path.Text = "Path";
            this.ch_Path.Width = 217;
            // 
            // ch_Before
            // 
            this.ch_Before.Text = "Before";
            this.ch_Before.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Before.Width = 179;
            // 
            // ch_After
            // 
            this.ch_After.Text = "After";
            this.ch_After.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_After.Width = 220;
            // 
            // ch_Result
            // 
            this.ch_Result.Text = "Result";
            this.ch_Result.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Result.Width = 129;
            // 
            // button_ResetList
            // 
            this.button_ResetList.Location = new System.Drawing.Point(135, 332);
            this.button_ResetList.Name = "button_ResetList";
            this.button_ResetList.Size = new System.Drawing.Size(75, 23);
            this.button_ResetList.TabIndex = 1;
            this.button_ResetList.Text = "Reset List";
            this.button_ResetList.UseVisualStyleBackColor = true;
            this.button_ResetList.Click += new System.EventHandler(this.button_ResetList_Click);
            // 
            // button_RemovefromList
            // 
            this.button_RemovefromList.Location = new System.Drawing.Point(12, 332);
            this.button_RemovefromList.Name = "button_RemovefromList";
            this.button_RemovefromList.Size = new System.Drawing.Size(117, 23);
            this.button_RemovefromList.TabIndex = 2;
            this.button_RemovefromList.Text = "Remove from List";
            this.button_RemovefromList.UseVisualStyleBackColor = true;
            this.button_RemovefromList.Click += new System.EventHandler(this.button_RemovefromList_Click);
            // 
            // listView_Rules
            // 
            this.listView_Rules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Rules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_Target,
            this.ch_Mode,
            this.ch_pa1,
            this.ch_pa2,
            this.ch_auxp});
            this.listView_Rules.FullRowSelect = true;
            this.listView_Rules.GridLines = true;
            this.listView_Rules.HideSelection = false;
            this.listView_Rules.Location = new System.Drawing.Point(561, 12);
            this.listView_Rules.Name = "listView_Rules";
            this.listView_Rules.Size = new System.Drawing.Size(435, 267);
            this.listView_Rules.TabIndex = 3;
            this.listView_Rules.UseCompatibleStateImageBehavior = false;
            this.listView_Rules.View = System.Windows.Forms.View.Details;
            this.listView_Rules.SelectedIndexChanged += new System.EventHandler(this.listView_Rules_SelectedIndexChanged);
            // 
            // ch_Target
            // 
            this.ch_Target.Text = "Target";
            this.ch_Target.Width = 69;
            // 
            // ch_Mode
            // 
            this.ch_Mode.Text = "Mode";
            this.ch_Mode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Mode.Width = 50;
            // 
            // ch_pa1
            // 
            this.ch_pa1.Text = "Parameter1";
            this.ch_pa1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_pa1.Width = 102;
            // 
            // ch_pa2
            // 
            this.ch_pa2.Text = "Parameter2";
            this.ch_pa2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_pa2.Width = 104;
            // 
            // ch_auxp
            // 
            this.ch_auxp.Text = "AuxParameter";
            this.ch_auxp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_auxp.Width = 84;
            // 
            // button_MoveUp
            // 
            this.button_MoveUp.Location = new System.Drawing.Point(495, 12);
            this.button_MoveUp.Name = "button_MoveUp";
            this.button_MoveUp.Size = new System.Drawing.Size(60, 60);
            this.button_MoveUp.TabIndex = 4;
            this.button_MoveUp.Text = "Move Up";
            this.button_MoveUp.UseVisualStyleBackColor = true;
            this.button_MoveUp.Click += new System.EventHandler(this.button_MoveUp_Click);
            // 
            // button_MoveDown
            // 
            this.button_MoveDown.Location = new System.Drawing.Point(495, 88);
            this.button_MoveDown.Name = "button_MoveDown";
            this.button_MoveDown.Size = new System.Drawing.Size(60, 60);
            this.button_MoveDown.TabIndex = 5;
            this.button_MoveDown.Text = "Move Down";
            this.button_MoveDown.UseVisualStyleBackColor = true;
            this.button_MoveDown.Click += new System.EventHandler(this.button_MoveDown_Click);
            // 
            // button_RemoveRule
            // 
            this.button_RemoveRule.Location = new System.Drawing.Point(495, 163);
            this.button_RemoveRule.Name = "button_RemoveRule";
            this.button_RemoveRule.Size = new System.Drawing.Size(60, 60);
            this.button_RemoveRule.TabIndex = 6;
            this.button_RemoveRule.Text = "Remove Rule";
            this.button_RemoveRule.UseVisualStyleBackColor = true;
            this.button_RemoveRule.Click += new System.EventHandler(this.button_RemoveRule_Click);
            // 
            // button_SaveProfile
            // 
            this.button_SaveProfile.Enabled = false;
            this.button_SaveProfile.Location = new System.Drawing.Point(936, 285);
            this.button_SaveProfile.Name = "button_SaveProfile";
            this.button_SaveProfile.Size = new System.Drawing.Size(60, 35);
            this.button_SaveProfile.TabIndex = 7;
            this.button_SaveProfile.Text = "Save Profile";
            this.button_SaveProfile.UseVisualStyleBackColor = true;
            this.button_SaveProfile.Click += new System.EventHandler(this.button_SaveProfile_Click);
            // 
            // button_AddRule
            // 
            this.button_AddRule.Location = new System.Drawing.Point(12, 243);
            this.button_AddRule.Name = "button_AddRule";
            this.button_AddRule.Size = new System.Drawing.Size(60, 35);
            this.button_AddRule.TabIndex = 8;
            this.button_AddRule.Text = "Add Rule";
            this.button_AddRule.UseVisualStyleBackColor = true;
            this.button_AddRule.Click += new System.EventHandler(this.button_AddRule_Click);
            // 
            // button_UpdateRule
            // 
            this.button_UpdateRule.Location = new System.Drawing.Point(12, 284);
            this.button_UpdateRule.Name = "button_UpdateRule";
            this.button_UpdateRule.Size = new System.Drawing.Size(60, 35);
            this.button_UpdateRule.TabIndex = 9;
            this.button_UpdateRule.Text = "Update Rule";
            this.button_UpdateRule.UseVisualStyleBackColor = true;
            this.button_UpdateRule.Click += new System.EventHandler(this.button_UpdateRule_Click);
            // 
            // label_Target
            // 
            this.label_Target.AutoSize = true;
            this.label_Target.Location = new System.Drawing.Point(26, 22);
            this.label_Target.Name = "label_Target";
            this.label_Target.Size = new System.Drawing.Size(76, 12);
            this.label_Target.TabIndex = 10;
            this.label_Target.Text = "Rename Target";
            // 
            // label_Mode
            // 
            this.label_Mode.AutoSize = true;
            this.label_Mode.Location = new System.Drawing.Point(26, 47);
            this.label_Mode.Name = "label_Mode";
            this.label_Mode.Size = new System.Drawing.Size(32, 12);
            this.label_Mode.TabIndex = 11;
            this.label_Mode.Text = "Mode";
            // 
            // label_Parameter1
            // 
            this.label_Parameter1.AutoSize = true;
            this.label_Parameter1.Location = new System.Drawing.Point(26, 73);
            this.label_Parameter1.Name = "label_Parameter1";
            this.label_Parameter1.Size = new System.Drawing.Size(60, 12);
            this.label_Parameter1.TabIndex = 12;
            this.label_Parameter1.Text = "Parameter 1";
            // 
            // label_Parameter2
            // 
            this.label_Parameter2.AutoSize = true;
            this.label_Parameter2.Location = new System.Drawing.Point(26, 102);
            this.label_Parameter2.Name = "label_Parameter2";
            this.label_Parameter2.Size = new System.Drawing.Size(60, 12);
            this.label_Parameter2.TabIndex = 13;
            this.label_Parameter2.Text = "Parameter 2";
            // 
            // comboBox_Target
            // 
            this.comboBox_Target.FormattingEnabled = true;
            this.comboBox_Target.Items.AddRange(new object[] {
            "FileName",
            "FileExtension"});
            this.comboBox_Target.Location = new System.Drawing.Point(135, 19);
            this.comboBox_Target.Name = "comboBox_Target";
            this.comboBox_Target.Size = new System.Drawing.Size(173, 20);
            this.comboBox_Target.TabIndex = 14;
            this.comboBox_Target.SelectedIndexChanged += new System.EventHandler(this.comboBox_Target_SelectedIndexChanged);
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "Prefix",
            "Suffix",
            "Remove",
            "Replace",
            "UPPERCASE",
            "lowercase",
            "Insert"});
            this.comboBox_Mode.Location = new System.Drawing.Point(135, 44);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(173, 20);
            this.comboBox_Mode.TabIndex = 15;
            this.comboBox_Mode.SelectedIndexChanged += new System.EventHandler(this.comboBox_Mode_SelectedIndexChanged);
            // 
            // comboBox_Parameter1
            // 
            this.comboBox_Parameter1.FormattingEnabled = true;
            this.comboBox_Parameter1.Location = new System.Drawing.Point(135, 70);
            this.comboBox_Parameter1.Name = "comboBox_Parameter1";
            this.comboBox_Parameter1.Size = new System.Drawing.Size(341, 20);
            this.comboBox_Parameter1.TabIndex = 16;
            this.comboBox_Parameter1.SelectedIndexChanged += new System.EventHandler(this.comboBox_Parameter1_SelectedIndexChanged);
            // 
            // comboBox_Parameter2
            // 
            this.comboBox_Parameter2.FormattingEnabled = true;
            this.comboBox_Parameter2.Location = new System.Drawing.Point(135, 99);
            this.comboBox_Parameter2.Name = "comboBox_Parameter2";
            this.comboBox_Parameter2.Size = new System.Drawing.Size(341, 20);
            this.comboBox_Parameter2.TabIndex = 17;
            // 
            // button_Preview
            // 
            this.button_Preview.Location = new System.Drawing.Point(416, 284);
            this.button_Preview.Name = "button_Preview";
            this.button_Preview.Size = new System.Drawing.Size(60, 35);
            this.button_Preview.TabIndex = 18;
            this.button_Preview.Text = "Preview";
            this.button_Preview.UseVisualStyleBackColor = true;
            this.button_Preview.Click += new System.EventHandler(this.button_Preview_Click);
            // 
            // button_Rename
            // 
            this.button_Rename.Location = new System.Drawing.Point(416, 320);
            this.button_Rename.Name = "button_Rename";
            this.button_Rename.Size = new System.Drawing.Size(60, 35);
            this.button_Rename.TabIndex = 18;
            this.button_Rename.Text = "Rename";
            this.button_Rename.UseVisualStyleBackColor = true;
            this.button_Rename.Click += new System.EventHandler(this.button_Rename_Click);
            // 
            // button_ResetRules
            // 
            this.button_ResetRules.Location = new System.Drawing.Point(495, 243);
            this.button_ResetRules.Name = "button_ResetRules";
            this.button_ResetRules.Size = new System.Drawing.Size(60, 35);
            this.button_ResetRules.TabIndex = 6;
            this.button_ResetRules.Text = "Reset Rules";
            this.button_ResetRules.UseVisualStyleBackColor = true;
            this.button_ResetRules.Click += new System.EventHandler(this.button_ResetRules_Click);
            // 
            // button_Undo
            // 
            this.button_Undo.Location = new System.Drawing.Point(350, 320);
            this.button_Undo.Name = "button_Undo";
            this.button_Undo.Size = new System.Drawing.Size(60, 35);
            this.button_Undo.TabIndex = 18;
            this.button_Undo.Text = "Undo";
            this.button_Undo.UseVisualStyleBackColor = true;
            this.button_Undo.Click += new System.EventHandler(this.button_Undo_Click);
            // 
            // label_AuxParameter1
            // 
            this.label_AuxParameter1.AutoSize = true;
            this.label_AuxParameter1.Location = new System.Drawing.Point(26, 131);
            this.label_AuxParameter1.Name = "label_AuxParameter1";
            this.label_AuxParameter1.Size = new System.Drawing.Size(80, 12);
            this.label_AuxParameter1.TabIndex = 12;
            this.label_AuxParameter1.Text = "AuxParameter 1";
            // 
            // label_AuxParameter2
            // 
            this.label_AuxParameter2.AutoSize = true;
            this.label_AuxParameter2.Location = new System.Drawing.Point(26, 160);
            this.label_AuxParameter2.Name = "label_AuxParameter2";
            this.label_AuxParameter2.Size = new System.Drawing.Size(77, 12);
            this.label_AuxParameter2.TabIndex = 13;
            this.label_AuxParameter2.Text = "AuxParameter2";
            // 
            // comboBox_AuxParameter1
            // 
            this.comboBox_AuxParameter1.FormattingEnabled = true;
            this.comboBox_AuxParameter1.Location = new System.Drawing.Point(135, 128);
            this.comboBox_AuxParameter1.Name = "comboBox_AuxParameter1";
            this.comboBox_AuxParameter1.Size = new System.Drawing.Size(173, 20);
            this.comboBox_AuxParameter1.TabIndex = 18;
            // 
            // comboBox_AuxParameter2
            // 
            this.comboBox_AuxParameter2.FormattingEnabled = true;
            this.comboBox_AuxParameter2.Location = new System.Drawing.Point(135, 157);
            this.comboBox_AuxParameter2.Name = "comboBox_AuxParameter2";
            this.comboBox_AuxParameter2.Size = new System.Drawing.Size(173, 20);
            this.comboBox_AuxParameter2.TabIndex = 19;
            // 
            // label_AuxParameter3
            // 
            this.label_AuxParameter3.AutoSize = true;
            this.label_AuxParameter3.Location = new System.Drawing.Point(26, 189);
            this.label_AuxParameter3.Name = "label_AuxParameter3";
            this.label_AuxParameter3.Size = new System.Drawing.Size(77, 12);
            this.label_AuxParameter3.TabIndex = 13;
            this.label_AuxParameter3.Text = "AuxParameter3";
            // 
            // comboBox_AuxParameter3
            // 
            this.comboBox_AuxParameter3.FormattingEnabled = true;
            this.comboBox_AuxParameter3.Location = new System.Drawing.Point(135, 186);
            this.comboBox_AuxParameter3.Name = "comboBox_AuxParameter3";
            this.comboBox_AuxParameter3.Size = new System.Drawing.Size(173, 20);
            this.comboBox_AuxParameter3.TabIndex = 20;
            // 
            // button_LoadProfile
            // 
            this.button_LoadProfile.Location = new System.Drawing.Point(870, 285);
            this.button_LoadProfile.Name = "button_LoadProfile";
            this.button_LoadProfile.Size = new System.Drawing.Size(60, 35);
            this.button_LoadProfile.TabIndex = 7;
            this.button_LoadProfile.Text = "Load Profile";
            this.button_LoadProfile.UseVisualStyleBackColor = true;
            this.button_LoadProfile.Click += new System.EventHandler(this.button_LoadProfile_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.button_Undo);
            this.Controls.Add(this.button_Rename);
            this.Controls.Add(this.button_Preview);
            this.Controls.Add(this.comboBox_AuxParameter3);
            this.Controls.Add(this.comboBox_AuxParameter2);
            this.Controls.Add(this.comboBox_Parameter2);
            this.Controls.Add(this.comboBox_AuxParameter1);
            this.Controls.Add(this.comboBox_Parameter1);
            this.Controls.Add(this.label_AuxParameter3);
            this.Controls.Add(this.comboBox_Mode);
            this.Controls.Add(this.label_AuxParameter2);
            this.Controls.Add(this.comboBox_Target);
            this.Controls.Add(this.label_AuxParameter1);
            this.Controls.Add(this.label_Parameter2);
            this.Controls.Add(this.label_Parameter1);
            this.Controls.Add(this.label_Mode);
            this.Controls.Add(this.label_Target);
            this.Controls.Add(this.button_UpdateRule);
            this.Controls.Add(this.button_AddRule);
            this.Controls.Add(this.button_LoadProfile);
            this.Controls.Add(this.button_SaveProfile);
            this.Controls.Add(this.button_ResetRules);
            this.Controls.Add(this.button_RemoveRule);
            this.Controls.Add(this.button_MoveDown);
            this.Controls.Add(this.button_MoveUp);
            this.Controls.Add(this.listView_Rules);
            this.Controls.Add(this.button_RemovefromList);
            this.Controls.Add(this.button_ResetList);
            this.Controls.Add(this.listView_FileList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "Rename My Ass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_FileList;
        private System.Windows.Forms.ColumnHeader ch_Path;
        private System.Windows.Forms.ColumnHeader ch_Before;
        private System.Windows.Forms.ColumnHeader ch_After;
        private System.Windows.Forms.ColumnHeader ch_Result;
        private System.Windows.Forms.Button button_ResetList;
        private System.Windows.Forms.Button button_RemovefromList;
        private System.Windows.Forms.ListView listView_Rules;
        private System.Windows.Forms.ColumnHeader ch_Target;
        private System.Windows.Forms.ColumnHeader ch_Mode;
        private System.Windows.Forms.ColumnHeader ch_pa1;
        private System.Windows.Forms.ColumnHeader ch_pa2;
        private System.Windows.Forms.Button button_MoveUp;
        private System.Windows.Forms.Button button_MoveDown;
        private System.Windows.Forms.Button button_RemoveRule;
        private System.Windows.Forms.Button button_SaveProfile;
        private System.Windows.Forms.Button button_AddRule;
        private System.Windows.Forms.Button button_UpdateRule;
        private System.Windows.Forms.Label label_Target;
        private System.Windows.Forms.Label label_Mode;
        private System.Windows.Forms.Label label_Parameter1;
        private System.Windows.Forms.Label label_Parameter2;
        private System.Windows.Forms.ComboBox comboBox_Target;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.ComboBox comboBox_Parameter1;
        private System.Windows.Forms.ComboBox comboBox_Parameter2;
        private System.Windows.Forms.Button button_Preview;
        private System.Windows.Forms.Button button_Rename;
        private System.Windows.Forms.Button button_ResetRules;
        private System.Windows.Forms.Button button_Undo;
        private System.Windows.Forms.Label label_AuxParameter1;
        private System.Windows.Forms.Label label_AuxParameter2;
        private System.Windows.Forms.ComboBox comboBox_AuxParameter1;
        private System.Windows.Forms.ComboBox comboBox_AuxParameter2;
        private System.Windows.Forms.Label label_AuxParameter3;
        private System.Windows.Forms.ComboBox comboBox_AuxParameter3;
        private System.Windows.Forms.ColumnHeader ch_auxp;
        private System.Windows.Forms.Button button_LoadProfile;
    }
}

