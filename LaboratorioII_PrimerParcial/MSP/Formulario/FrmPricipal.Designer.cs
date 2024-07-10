namespace Formulario
{
    partial class FrmPricipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSql = new System.Windows.Forms.Button();
            this.btnXml = new System.Windows.Forms.Button();
            this.btnTxt = new System.Windows.Forms.Button();
            this.vistaPatente = new Patentes.VistaPatente();
            this.btnMas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSql
            // 
            this.btnSql.Location = new System.Drawing.Point(91, 83);
            this.btnSql.Name = "btnSql";
            this.btnSql.Size = new System.Drawing.Size(77, 38);
            this.btnSql.TabIndex = 7;
            this.btnSql.Text = "SQL";
            this.btnSql.UseVisualStyleBackColor = true;
            this.btnSql.Click += new System.EventHandler(this.btnSql_Click);
            // 
            // btnXml
            // 
            this.btnXml.Location = new System.Drawing.Point(12, 127);
            this.btnXml.Name = "btnXml";
            this.btnXml.Size = new System.Drawing.Size(77, 38);
            this.btnXml.TabIndex = 6;
            this.btnXml.Text = "XML";
            this.btnXml.UseVisualStyleBackColor = true;
            this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
            // 
            // btnTxt
            // 
            this.btnTxt.Location = new System.Drawing.Point(91, 127);
            this.btnTxt.Name = "btnTxt";
            this.btnTxt.Size = new System.Drawing.Size(77, 38);
            this.btnTxt.TabIndex = 5;
            this.btnTxt.Text = "TXT";
            this.btnTxt.UseVisualStyleBackColor = true;
            this.btnTxt.Click += new System.EventHandler(this.btnTxt_Click);
            // 
            // vistaPatente
            // 
            this.vistaPatente.Location = new System.Drawing.Point(23, 12);
            this.vistaPatente.Name = "vistaPatente";
            this.vistaPatente.Size = new System.Drawing.Size(133, 54);
            this.vistaPatente.TabIndex = 8;
            // 
            // btnMas
            // 
            this.btnMas.Location = new System.Drawing.Point(12, 83);
            this.btnMas.Name = "btnMas";
            this.btnMas.Size = new System.Drawing.Size(77, 38);
            this.btnMas.TabIndex = 9;
            this.btnMas.Text = "+";
            this.btnMas.UseVisualStyleBackColor = true;
            this.btnMas.Click += new System.EventHandler(this.btnMas_Click);
            // 
            // FrmPricipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 175);
            this.Controls.Add(this.btnMas);
            this.Controls.Add(this.vistaPatente);
            this.Controls.Add(this.btnSql);
            this.Controls.Add(this.btnXml);
            this.Controls.Add(this.btnTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPricipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPricipal_FormClosing);
            this.Load += new System.EventHandler(this.FrmPricipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSql;
        private System.Windows.Forms.Button btnXml;
        private System.Windows.Forms.Button btnTxt;
        private Patentes.VistaPatente vistaPatente;
        private System.Windows.Forms.Button btnMas;
    }
}

