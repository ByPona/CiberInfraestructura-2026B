
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaInventario
{
    public partial class Form1 : Form
    {
        // Configuración de la conexión a MySQL.
        private readonly string connectionString =
            "Server=localhost;Database=EmpresaInventario;Uid=root;Pwd=219748227Fcg0209#;";

        private ComboBox cmbProducto = new ComboBox();
        private NumericUpDown numCantidad = new NumericUpDown();
        private DataGridView dgvInventario = new DataGridView();

        private Button btnRegistrar = new Button();
        private Button btnConsultar = new Button();
        private Button btnEliminar = new Button();

        public Form1()
        {
            InitializeComponent();

            CrearInterfaz();

            // Conectar los eventos de los botones.
            btnRegistrar.Click += btnRegistrar_Click;
            btnConsultar.Click += btnConsultar_Click;
            btnEliminar.Click += btnEliminar_Click;

            // Cargar los datos al abrir la ventana.
            Load += Form1_Load;
        }

        private void CrearInterfaz()
        {
            Text = "Sistema de Inventario";
            ClientSize = new Size(950, 620);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 500);

            // Etiqueta y selector de producto.
            var lblProducto = new Label
            {
                Text = "Producto:",
                Location = new Point(35, 35),
                AutoSize = true
            };

            cmbProducto.SetBounds(150, 30, 260, 30);
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;

            // Etiqueta y selector de cantidad.
            var lblCantidad = new Label
            {
                Text = "Cantidad:",
                Location = new Point(440, 35),
                AutoSize = true
            };

            numCantidad.SetBounds(555, 30, 130, 30);
            numCantidad.Minimum = 1;
            numCantidad.Maximum = 100000;
            numCantidad.Value = 1;

            // Botón para registrar.
            btnRegistrar.Text = "Registrar";
            btnRegistrar.SetBounds(35, 90, 140, 40);

            // Botón para consultar.
            btnConsultar.Text = "Consultar";
            btnConsultar.SetBounds(195, 90, 140, 40);

            // Botón para eliminar.
            btnEliminar.Text = "Eliminar seleccionado";
            btnEliminar.SetBounds(355, 90, 190, 40);

            // Tabla de inventario.
            dgvInventario.SetBounds(35, 165, 880, 420);
            dgvInventario.ReadOnly = true;
            dgvInventario.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.MultiSelect = false;
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventario.RowHeadersVisible = false;
            dgvInventario.AutoGenerateColumns = true;

            // Agregar controles a la ventana.
            Controls.AddRange(new Control[]
            {
                lblProducto,
                cmbProducto,
                lblCantidad,
                numCantidad,
                btnRegistrar,
                btnConsultar,
                btnEliminar,
                dgvInventario
            });
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                CargarProductos();
                CargarInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible cargar los datos.\n\n" + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Cargar los productos disponibles en el ComboBox.
        private void CargarProductos()
        {
            using var conexion = new MySqlConnection(connectionString);

            string consulta = @"
                SELECT ID, `Descripción`
                FROM CatProducto
                ORDER BY `Descripción`;";

            using var adaptador = new MySqlDataAdapter(consulta, conexion);

            var tabla = new DataTable();
            adaptador.Fill(tabla);

            cmbProducto.DataSource = null;
            cmbProducto.DisplayMember = "Descripción";
            cmbProducto.ValueMember = "ID";
            cmbProducto.DataSource = tabla;
        }

        // Consultar y mostrar los registros del inventario.
        private void CargarInventario()
        {
            using var conexion = new MySqlConnection(connectionString);

            string consulta = @"
                SELECT
                    i.ID AS 'ID Inventario',
                    i.FechaAlta AS 'Fecha de alta',
                    p.`Descripción` AS 'Producto',
                    i.Cantidad AS 'Cantidad'
                FROM tblInventario i
                INNER JOIN CatProducto p
                    ON i.IDProducto = p.ID
                ORDER BY i.ID DESC;";

            using var adaptador = new MySqlDataAdapter(consulta, conexion);

            var tabla = new DataTable();
            adaptador.Fill(tabla);

            dgvInventario.DataSource = tabla;
        }

        // Registrar una entrada en el inventario.
        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null)
            {
                MessageBox.Show(
                    "Primero debes registrar productos en el catálogo CatProducto.",
                    "Producto no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                using var conexion = new MySqlConnection(connectionString);
                conexion.Open();

                string consulta = @"
                    INSERT INTO tblInventario (IDProducto, Cantidad)
                    VALUES (@idProducto, @cantidad);";

                using var comando = new MySqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue(
                    "@idProducto",
                    Convert.ToInt32(cmbProducto.SelectedValue)
                );

                comando.Parameters.AddWithValue(
                    "@cantidad",
                    Convert.ToInt32(numCantidad.Value)
                );

                comando.ExecuteNonQuery();

                MessageBox.Show(
                    "Producto registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Actualizar la tabla inmediatamente.
                CargarInventario();

                // Restablecer la cantidad.
                numCantidad.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible registrar el producto.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Actualizar manualmente la tabla.
        private void btnConsultar_Click(object? sender, EventArgs e)
        {
            try
            {
                CargarInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible consultar el inventario.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Eliminar el registro seleccionado.
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null ||
                dgvInventario.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Selecciona un registro de la tabla.",
                    "Sin selección",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            object? valorId =
                dgvInventario.CurrentRow.Cells["ID Inventario"].Value;

            if (valorId == null || valorId == DBNull.Value)
            {
                MessageBox.Show("No se pudo obtener el ID del registro.");
                return;
            }

            int id = Convert.ToInt32(valorId);

            DialogResult resultado = MessageBox.Show(
                "¿Deseas eliminar el registro seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                using var conexion = new MySqlConnection(connectionString);
                conexion.Open();

                string consulta =
                    "DELETE FROM tblInventario WHERE ID = @id;";

                using var comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();

                MessageBox.Show(
                    "Registro eliminado correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible eliminar el registro.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
