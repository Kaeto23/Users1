using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Users
{
    public partial class Form1 : Form
    {
        portaltable stmodel = new portaltable();
        public Form1()
        {
            InitializeComponent();
            populateDataGridView();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            populateDataGridView();
        }
        private void populateDataGridView()
        {
            portalEntities db = new portalEntities();
            dataGridView1.DataSource = db.portaltable.ToList<portaltable>();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (portalEntities portal = new portalEntities())
            {
                try
                {
                    stmodel.Username = txtboxUsername.Text;
                    stmodel.Email = txtboxEmailAddress.Text;
                    stmodel.Name = txtboxName.Text;
                    stmodel.FamilyName = txtboxFamilyName.Text;
                    stmodel.Phone = txtboxPhone.Text;
                    stmodel.Mobile = txtboxMobile.Text;
                    stmodel.BirthDate = txtboxBirthDate.Text;
                    stmodel.BirthPlace = txtboxBirthPlace.Text;

                    // Attach the entity if it's new
                    if (stmodel.ID == 0)
                    {
                        portal.portaltable.Add(stmodel);
                    }
                    else
                    {
                        portal.Entry(stmodel).State = System.Data.Entity.EntityState.Modified;
                    }

                    portal.SaveChanges();
                    MessageBox.Show("Data Edited successfully.");
                    populateDataGridView();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Handle concurrency exception
                    MessageBox.Show("The record you attempted to edit was modified by another user. Please reload the data and try again.");

                    // Optionally, you can refresh the entity
                    var entry = ex.Entries.Single();
                    var databaseEntry = entry.GetDatabaseValues();
                    entry.Reload();
                }
            }
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if (stmodel.ID == 0)
            {
                MessageBox.Show("No record selected for deletion.");
                return;
            }

            using (portalEntities db = new portalEntities())
            {
                try
                {
                    var recordToDelete = db.portaltable.Find(stmodel.ID);
                    if (recordToDelete != null)
                    {
                        db.portaltable.Remove(recordToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Data Deleted successfully.");
                        populateDataGridView();
                        buttonClear_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Record not found.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception
                    MessageBox.Show("The record you attempted to delete was modified by another user. Please reload the data and try again.");
                }
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            portalEntities portal = new portalEntities();

            stmodel.Username = txtboxUsername.Text;
            stmodel.Email = txtboxEmailAddress.Text;
            stmodel.Name = txtboxName.Text;
            stmodel.FamilyName = txtboxFamilyName.Text;
            stmodel.Phone = txtboxPhone.Text;
            stmodel.Mobile = txtboxMobile.Text;
            stmodel.BirthDate = txtboxBirthDate.Text;
            stmodel.BirthPlace = txtboxBirthPlace.Text;
            if (stmodel.ID == 0)
            {
                portal.portaltable.Add(stmodel);
            }
            else

                portal.Entry(stmodel).State = System.Data.Entity.EntityState.Modified;
            portal.SaveChanges();
            MessageBox.Show("Data Registered successfully.");

            populateDataGridView();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                stmodel.ID = (int)dataGridView1.CurrentRow.Cells["ID"].Value;
                portalEntities db = new portalEntities();
                stmodel = db.portaltable.Where(x => x.ID == stmodel.ID).FirstOrDefault();
                txtboxUsername.Text = stmodel.Username;
                txtboxEmailAddress.Text = stmodel.Email;
                txtboxName.Text = stmodel.Name;
                txtboxFamilyName.Text = stmodel.FamilyName;
                txtboxPhone.Text = stmodel.Phone;
                txtboxMobile.Text = stmodel.Mobile;
                txtboxBirthDate.Text = stmodel.BirthDate;
                txtboxBirthPlace.Text = stmodel.BirthPlace;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtboxUsername.Text = string.Empty;
            txtboxEmailAddress.Text = string.Empty;
            txtboxName.Text = string.Empty;
            txtboxFamilyName.Text = string.Empty;
            txtboxPhone.Text = string.Empty;
            txtboxMobile.Text = string.Empty;
            txtboxBirthDate.Text = string.Empty;
            txtboxBirthPlace.Text = string.Empty;
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            portalEntities db = new portalEntities();
            dataGridView1.DataSource = db.portaltable.Where(x=> x.Username.Contains(txtboxSearch.Text)).ToList();

        }
    }
}

 