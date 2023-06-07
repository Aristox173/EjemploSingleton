using System;
using System.Data;
using System.Data.SqlClient;

public class Singleton
{
    private static Singleton instance;
    private SqlConnection connection;

    private Singleton()
    {
        string connectionString = "Data Source=(local); Initial Catalog=TiendaMascotasDB; trusted_connection=yes; TrustServerCertificate=True;";
        connection = new SqlConnection(connectionString);
    }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    public void OpenConnection()
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
    }

    public void DisplayData()
    {
        OpenConnection();
        // Aquí puedes ejecutar una consulta SQL y mostrar los datos en pantalla
        string query = "SELECT * FROM dbo.Customers"; // Actualiza con el nombre de tu tabla real
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            // Leer y mostrar los datos
            int id = reader.GetInt32(0); // Reemplaza 0 con el índice de columna correcto
            string Name = reader.GetString(1); // Reemplaza 1 con el índice de columna correcto
            string Description = reader.GetString(2); // Reemplaza 2 con el índice de columna correcto
            string Price = reader.GetString(3); // Reemplaza 3 con el índice de columna correcto
            string Quantity = reader.GetString(4); // Reemplaza 4 con el índice de columna correcto

            Console.WriteLine("ID: {0}, Name: {1}, Description: {2}, Price: {3}, Quantity: {4}", id, Name, Description, Price, Quantity);
        }

        reader.Close();
        CloseConnection();
    }
}

public class Program
{
    public static void Main()
    {
        // Obtener la instancia de Singleton
        Singleton dbConnection = Singleton.Instance;

        // Mostrar los datos en pantalla
        dbConnection.DisplayData();

        // Esperar a que el usuario presione una tecla antes de salir
        Console.ReadKey();
    }
}