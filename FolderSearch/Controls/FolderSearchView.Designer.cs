namespace FolderSearch.Controls
{
	partial class FolderSearchView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tvwFolders = new System.Windows.Forms.TreeView();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tvwFolders
			// 
			this.tvwFolders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwFolders.Location = new System.Drawing.Point(0, 21);
			this.tvwFolders.Name = "tvwFolders";
			this.tvwFolders.Size = new System.Drawing.Size(282, 276);
			this.tvwFolders.TabIndex = 3;
			// 
			// tbSearch
			// 
			this.tbSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.tbSearch.Location = new System.Drawing.Point(0, 0);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(282, 21);
			this.tbSearch.TabIndex = 2;
			// 
			// FolderSearchView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tvwFolders);
			this.Controls.Add(this.tbSearch);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FolderSearchView";
			this.Size = new System.Drawing.Size(282, 297);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView tvwFolders;
		private System.Windows.Forms.TextBox tbSearch;
	}
}
