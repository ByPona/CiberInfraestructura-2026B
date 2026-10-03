using System.Text.RegularExpressions;

namespace HolaMundo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Asociar el botón con el evento Click
            button1.Click += button1_Click;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            string password = textBox1.Text;
            string confirmPassword = textBox2.Text;

            // La contraseña debe contener:
            // - Al menos una letra mayúscula
            // - Al menos una letra minúscula
            // - Al menos un símbolo
            // - Al menos un número
            string patron = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$";

            // Primera validación: estructura de la contraseña
            if (!Regex.IsMatch(password, patron))
            {
                MessageBox.Show(
                    "La contraseña debe contener al menos una letra mayúscula, " +
                    "una letra minúscula, un símbolo y un número.",
                    "Contraseña inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Segunda validación: comprobar que ambas contraseñas coincidan
            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Las contraseñas no coinciden.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // La contraseña cumple todas las condiciones
            MessageBox.Show(
                "La contraseña ha sido validada",
                "Validación exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}