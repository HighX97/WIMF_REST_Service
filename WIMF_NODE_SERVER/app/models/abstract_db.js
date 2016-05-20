function insert(table, columns, values)
{
    var query = "INSERT INTO " + table + " ";
    var i = 1;
    for (x in columns)
    {
      if (i == 1)
      {
        query += "(";
      }
      query += columns[x]+" ";
      if (i < columns.length)
      {
        query += ",";
      }
      if (i == columns.length)
      {
        query += ") ";
      }
      i++;
    }
    query += "VALUES ";
    i = 1;
    for (x in values)
    {
      if (i == 1)
      {
        query += "(";
      }
      query += values[x]+" ";
      if (i < values.length)
      {
        query += ",";
      }
      if (i == values.length)
      {
        query += ") ";
      }
      i++;
    }
    return query;
}
