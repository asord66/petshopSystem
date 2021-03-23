using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProdutosPetShop
{
    public partial class Sistema : Form
    {
        public Sistema()
        {
            InitializeComponent();
            tabControl1.Selected += new TabControlEventHandler(tabControl1_SelectedIndexChanged);
        }
        private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                marcadorLateral.Height = btnCadastra.Height;
                marcadorLateral.Top = btnCadastra.Top;
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                marcadorLateral.Height = btnBusca.Height;
                marcadorLateral.Top = btnBusca.Top;
            }
            else
            {
                marcadorLateral.Height = btnAlterar.Height;
                marcadorLateral.Top = btnAlterar.Top;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*---------------------------------------------------------------*/

        //adicionando transição de um botão para outro através de um panel(marcadorLateral)
        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcadorLateral.Height = btnCadastra.Height;
            marcadorLateral.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0]; //tabPage na posição 0
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcadorLateral.Height = btnBusca.Height;
            marcadorLateral.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1]; //tabPage na posição 1
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
        }
        /*---------------------------------------------------------------*/
        private void btnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            //set dos campos à classe Produto
            Produto produto = new Produto();

            produto.Nome = txtNome.Text;
            produto.Descricao = txtDescricao.Text;
            produto.Marca = txtMarca.Text;
            produto.Quant = Convert.ToInt32(txtQtd.Text);
            produto.ValorCompra = Convert.ToSingle(txtValorCompra.Text);
            produto.ValorVenda = Convert.ToSingle(txtValorUni.Text);

            ConectaBanco con = new ConectaBanco();
            bool r = con.cadastraProdutos(produto);

            if(r == true) MessageBox.Show("Produto inserido com sucesso!");
            else MessageBox.Show("Error " + con.mensagem);

            listaProdutos();
            formClear();
        }

        private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
            Produto p = new Produto();

            p.Nome = txtAlteraNome.Text;
            p.Descricao = txtAlteraDescricao.Text;
            p.Marca = txtAlteraMarca.Text;
            p.Quant = Convert.ToInt32(txtAlteraQtd.Text);
            p.ValorCompra = Convert.ToDouble(txtAlteraValorCompra.Text);
            p.ValorVenda = Convert.ToDouble(txtAlteraValorUni.Text);

            ConectaBanco con = new ConectaBanco();

            if (con.alteraProduto(p, Convert.ToInt32(dgProdutos.CurrentRow.Cells[0].Value.ToString())))

                MessageBox.Show("Produto alterado!");
            else
                MessageBox.Show("Falha ao alterar o produto, " + con.mensagem);

            formClear();
            listaProdutos();
            tabControl1.SelectedTab = tabBuscar;
        }
        private void btnConfirmaRemocao_Click(object sender, EventArgs e)
        {
            int idDeleta = Convert.ToInt32(dgProdutos.CurrentRow.Cells[0].Value.ToString());

            DialogResult resposta = MessageBox.Show("Tem certeza que deseja deletar?", "Delete", MessageBoxButtons.OKCancel);
            if (resposta == DialogResult.OK) {
                ConectaBanco con = new ConectaBanco();
                bool retorno = con.deletaProduto(idDeleta);

                if (retorno)
                    MessageBox.Show("Produto deletado com sucesso!");
                else
                    MessageBox.Show(con.mensagem);

                listaProdutos();
            }
        }
        /*---------------------------------------------------------------*/
        
        //função para limpar o form após o cadastro
        void formClear()
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtMarca.Clear();
            txtQtd.Clear();
            txtValorCompra.Clear();
            txtValorUni.Clear();
            txtNome.Focus();

            txtAlteraNome.Clear();
            txtAlteraDescricao.Clear();
            txtAlteraMarca.Clear();
            txtAlteraQtd.Clear();
            txtAlteraValorCompra.Clear();
            txtAlteraValorUni.Clear();


        }
        void listaProdutos()
        {
            //chamando o banco no formulário com instância da calsse
            ConectaBanco con = new ConectaBanco();
            dgProdutos.DataSource = con.listaProdutos(); //DataGridView

           // dgProdutos.Columns[0].Visible = false;
        }

        private void Sistema_Load(object sender, EventArgs e)
        {
            //quando abrir o formulário, é chamada a função listaProdutos
            listaProdutos();
        }

        private void dgProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNomeBusca_TextChanged(object sender, EventArgs e)
        {
            (dgProdutos.DataSource as DataTable).DefaultView.RowFilter = string.Format("nome like '%{0}%'", txtNomeBusca.Text);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            marcadorLateral.Height = btnAlterar.Height;
            marcadorLateral.Top = btnAlterar.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[2]; 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
            txtAlteraNome.Text = dgProdutos.CurrentRow.Cells[1].Value.ToString();
            txtAlteraDescricao.Text = dgProdutos.CurrentRow.Cells[2].Value.ToString();
            txtAlteraMarca.Text = dgProdutos.CurrentRow.Cells[3].Value.ToString();
            txtAlteraQtd.Text = dgProdutos.CurrentRow.Cells[4].Value.ToString();
            txtAlteraValorCompra.Text = dgProdutos.CurrentRow.Cells[5].Value.ToString();
            txtAlteraValorUni.Text = dgProdutos.CurrentRow.Cells[6].Value.ToString();

        }
    }
}
