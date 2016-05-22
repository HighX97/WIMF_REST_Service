var mysql = require('mysql');

var abstract_db = function()
{

  this.connection = mysql.createConnection({
    host : 'localhost',
    user : 'jimmy',
    password : 'ikbal',
    database :  'db_wimf'
  })

  this.mysql_delete= function(table,where)
  {
    var query = "DELETE FROM " + table + " ";
    query += "WHERE " + where;
    console.log(new Date()+" : ");
    console.log(query);
    return query;
  }

  this.mysql_update =function (table,set,where)
  {
    var query = "UPDATE " + table + " SET ";
    var i = 1;
    for (x in set)
    {
      query += set[x]+" ";
      if (i < set.length)
      {
        query += ",";
      }
      i++;
    }
    query += "WHERE " + where;
    console.log(new Date()+" : ");
    console.log(query);
    return query;
  }

  this.mysql_select = function(select,from,where,orderby)
  {
    var query = "SELECT " + select + " FROM ";
    var i = 1;
    for (x in from)
    {
      query += from[x]+" ";
      if (i < from.length)
      {
        query += ",";
      }
      i++;
    }
    if(where != "")
    {
      query += "WHERE " + where + " ";
    }
    if(orderby != "")
    {
      query += "ORDER BY " + orderby;
    }
    console.log(new Date()+" : ");
    console.log(query);
    return query;
  }

  this.mysql_insert = function(table, columns, values)
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
    console.log(new Date()+" : ");
    console.log(query);
    return query;
  }
}

module.exports = new abstract_db();
