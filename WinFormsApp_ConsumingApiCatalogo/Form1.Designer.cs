namespace WinFormsApp_ConsumingApiCatalogo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAutenticar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURI = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnGetById = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAutenticar
            // 
            this.btnAutenticar.Location = new System.Drawing.Point(626, 17);
            this.btnAutenticar.Name = "btnAutenticar";
            this.btnAutenticar.Size = new System.Drawing.Size(133, 23);
            this.btnAutenticar.TabIndex = 0;
            this.btnAutenticar.Text = "Autenticar";
            this.btnAutenticar.UseVisualStyleBackColor = true;
            this.btnAutenticar.Click += new System.EventHandler(this.btnAutenticar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "URI -Web Api";
            // 
            // txtURI
            // 
            this.txtURI.Location = new System.Drawing.Point(132, 17);
            this.txtURI.Name = "txtURI";
            this.txtURI.Size = new System.Drawing.Size(475, 23);
            this.txtURI.TabIndex = 2;
            this.txtURI.Text = "https://localhost:7150/api/v1/produtos";
            // 
            // dgvDados
            // 
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(30, 68);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.RowTemplate.Height = 25;
            this.dgvDados.Size = new System.Drawing.Size(729, 315);
            this.dgvDados.TabIndex = 3;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(30, 402);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(130, 38);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "Obter Produtos";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnGetById
            // 
            this.btnGetById.Location = new System.Drawing.Point(179, 402);
            this.btnGetById.Name = "btnGetById";
            this.btnGetById.Size = new System.Drawing.Size(130, 38);
            this.btnGetById.TabIndex = 5;
            this.btnGetById.Text = "Obter produtos por ID";
            this.btnGetById.UseVisualStyleBackColor = true;
            this.btnGetById.Click += new System.EventHandler(this.btnGetById_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(328, 402);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 38);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Incluir Produto";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(477, 402);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 38);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Atualizar Produto";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(626, 402);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(130, 38);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "Apagar Produto";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 470);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnGetById);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.dgvDados);
            this.Controls.Add(this.txtURI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAutenticar);
            this.Name = "Form1";
            this.Text = "Cosuming APiCatalogo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnAutenticar;
        private Label label1;
        private TextBox txtURI;
        private DataGridView dgvDados;
        private Button btnGet;
        private Button btnGetById;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDel;
    }
}