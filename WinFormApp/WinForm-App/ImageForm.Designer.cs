﻿namespace WinForm_App
{
    partial class ImageForm
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
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.LblImageUrl = new System.Windows.Forms.Label();
            this.TxtBoxImageUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Location = new System.Drawing.Point(182, 147);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(91, 36);
            this.BtnAgregar.TabIndex = 0;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = true;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(80, 147);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(96, 36);
            this.BtnCancelar.TabIndex = 1;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            // 
            // LblImageUrl
            // 
            this.LblImageUrl.AutoSize = true;
            this.LblImageUrl.Location = new System.Drawing.Point(26, 96);
            this.LblImageUrl.Name = "LblImageUrl";
            this.LblImageUrl.Size = new System.Drawing.Size(77, 16);
            this.LblImageUrl.TabIndex = 4;
            this.LblImageUrl.Text = "Imagen (url)";
            // 
            // TxtBoxImageUrl
            // 
            this.TxtBoxImageUrl.Location = new System.Drawing.Point(109, 93);
            this.TxtBoxImageUrl.Name = "TxtBoxImageUrl";
            this.TxtBoxImageUrl.Size = new System.Drawing.Size(193, 22);
            this.TxtBoxImageUrl.TabIndex = 5;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 222);
            this.Controls.Add(this.TxtBoxImageUrl);
            this.Controls.Add(this.LblImageUrl);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnAgregar);
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            this.Load += new System.EventHandler(this.ImageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Label LblImageUrl;
        private System.Windows.Forms.TextBox TxtBoxImageUrl;
    }
}