using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace WindowsFormsApp2 {
    class Database {

        private static SqlConnection c;
        private static SqlCommand cmd;
        private static SqlDataReader dr;

        public static bool Init() {
            try {
                c = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JooJ\Documents\Database.mdf;Integrated Security=True;Connect Timeout=30");
                cmd = new SqlCommand();
                c.Open();
                return true;
            } catch (Exception e) {
                Logger.Log(e.ToString());
                return false;
            }
        }

        public static DataTable FetchTable(String dataname) {
            cmd = new SqlCommand("SELECT * FROM " + dataname, c);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtr = new DataTable();
            da.Fill(dtr);
            return dtr;
        }

        public static DataTable CustomFetchTable(String sqlCommand) {
            cmd = new SqlCommand(sqlCommand, c);
            DataTable dst = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dst);
            return dst;

        }

        public static List<String> getColumnsName(String table) {
            List<String> list = new List<string>();
            cmd.CommandText = "SELECT c.name from sys.columns c INNER JOIN sys.tables t ON t.object_id = c.object_id and t.name = '" + table + "' AND t.type = 'U'";
            using (var reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        public static List<object> getColumnsType(String table) {
            List<object> l = new List<object>();
            DataTable columns = c.GetSchema("Columns", new[] { null, null, table, null });
            foreach (DataRow r in columns.Rows) {
                l.Add(r["DATA_TYPE"]);
            }
            return l;
        }

        public static List<String> getColumnsTypeName(String table) {
            List<String> l = new List<String>();
            DataTable columns = c.GetSchema("columns", new[] { null, null, table, null });
            foreach (DataRow r in columns.Rows) {
                l.Add(r["DATA_TYPE"].ToString());
            }
            return l;
        }

        public static int insertOnTable(String table, List<String> fieldNames, List<object> values) {
            String command = "INSERT INTO " + table + " (";
            foreach (String fn in fieldNames) {
                if (!command.EndsWith("("))
                    command += ", ";
                command += fn;
            }
            command += ") VALUES (";
            for (int i = 0; i < values.Count; i++) {
                if (!command.EndsWith("("))
                    command += ", ";
                command += "@val" + i;
            }
            command += ")";
            try {
                cmd = new SqlCommand(command, c);
                for (int i = 0; i < values.Count; i++) {   
                    cmd.Parameters.AddWithValue("@val" + i, values[i]);
                }
                return cmd.ExecuteNonQuery();
            } catch (Exception) {
                return 0;
            }
        }

        public static int updateFromTable(String table, int id, List<String> fieldNames, List<object> values) {

            String command = "UPDATE " + table + " SET ";
            for (int i = 0; i < values.Count; i++) {
                if (i != 0)
                    command += ",";
                command += " " + fieldNames[i] + " = @val" + i + " ";
            }
            command += "WHERE Id = @id";
            Debug.WriteLine(command);
            try {
                cmd = new SqlCommand(command, c);
                for (int i = 0; i < values.Count; i++) {
                    cmd.Parameters.AddWithValue("@val" + i, values[i]);
                }
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            } catch(Exception) {
                return 0;
            }
        }

        public static int removeFromTable(String table, int id) {
            String command = "DELETE FROM " + table + " WHERE Id = @id";
            try {
                cmd = new SqlCommand(command, c);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            } catch (Exception) {
                return 0;
            }
        }

        public static List<object> getAllValuesFromId(String table, int id, String idName = "id") {
            try {
                List<object> res = new List<object>();
                int wide = getColumnsTypeName(table).Count;
                cmd = new SqlCommand("SELECT * FROM " + table + " WHERE " + idName + " = " + id, c);
                using (dr = cmd.ExecuteReader()) {
                    if (dr.HasRows) {
                        while (dr.Read()) {
                            for (int i = 0; i < wide; i++) {
                                res.Add(dr.GetValue(i));
                            }
                        }
                    } else return null;
                }
                res.RemoveAt(0);
                return res;
            } catch (Exception) {
                return null;
            }
        }

        public static List<object> getAllValuesOrAListOfThemUsingEspecificIndex(String table, String index, String indexvalue, String valuelist = "*") {
            try {
                List<object> res = new List<object>();
                int wide = valuelist.TakeWhile(c => c == ',').Count() + 1;
                cmd = new SqlCommand("SELECT " + valuelist + " FROM " + table + " WHERE " + index + " = @val0", c);
                cmd.Parameters.AddWithValue("@val0", indexvalue);
                using (dr = cmd.ExecuteReader()) {
                    if (dr.HasRows) {
                        while (dr.Read()) {
                            for (int i = 0; i < wide; i++) {
                                res.Add(dr.GetValue(i));
                            }
                        }
                    } else return null;
                }
                return res;
            } catch (Exception) {
                return null;
            }
        }
    }

    public enum QueryType {
        INSERT, SELECT, UPDATE, DELETE
    }
}
 