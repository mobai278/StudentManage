1. 数据库设计

1.1 院系表

基础数据，存放院系相关信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps1.jpg) 

 

1.2 专业表

基础数据，存放专业相关信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps2.jpg) 

 

1.3 菜单（模块）表

基础数据，存放各个模块的信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps3.jpg) 

 

1.4 系统用户表

用户信息，存放管理人员用户详细信息

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps4.jpg) 

 

1.5 角色表

存放角色的详细信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps5.jpg) 

 

1.6 多对多关系存储表

多对多映射关系表，比如UserRole的关联关系可以存放在此表中。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps6.jpg) 

 

1.7 教师信息存储表

用来存放教师相关的详细信息

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps7.jpg) 

 

1.8 学生信息存储表

用来存放学生信息的详情

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps8.jpg) 

 

1.9 授课信息存储表

用来存放学校的授课信息，可在本系统维护。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps9.jpg) 

 

1.10 班级信息表

学校班级详细信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps10.jpg) 

 

1.11 课程信息表

学校课程详细信息。

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps11.jpg) 

 

1.12 成绩存储表

学生的成绩表

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps12.jpg) 

 

2. 框架结构设计

![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps13.jpg) 

 

3. 项目结构

   ![img](file:///C:\Users\86138\AppData\Local\Temp\ksohtml7212\wps14.jpg)

 

 

**六、*****\*项目的实现\****

1. 数据库连接

  

```C#
public IConfiguration Config;
public SqlConnection Connection
   {
    get
    {
       return new SqlConnection(Config.GetConnectionString("DefaultConnection"));
    }
 }
```

 

 

2. 增删改查基类方法的实现如下：

```C#
    /// <summary>
    /// 异步查询
    /// </summary>
    /// <returns></returns>
    public async Task<TU> ExecuteScalarAsync<TU>(string sql, object param = null)
    {
      using (var conn = Connection)
      {
        await conn.OpenAsync();
        return await conn.ExecuteScalarAsync<TU>(sql, param);
      }
    }
 

    /// <summary>
    /// 异步查询
    /// </summary>
    /// <param name="sql">sql字符串</param>
    /// <param name="param">参数</param>
    /// <returns>异步返回int</returns>
    public async Task<int> ExecuteAsync(string sql, object param = null)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.ExecuteAsync(sql, param);
      }
    }

 

    /// <summary>
    /// 新增
    /// </summary>
    public async Task<int> AddAsync(T t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.InsertAsync<T>(t);
      }
    }
 
    /// <summary>
    /// 批量新增
    /// </summary>
    public async Task<int> AddListAsync(List<T> t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.InsertAsync<List<T>>(t);
      }
    }
 

    /// <summary>
    /// 异步更新
    /// </summary>
    /// <param name="t">表实体</param>
    public async Task<bool> UpdateAsync(T t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.UpdateAsync<T>(t);
      }
    }

 

    /// <summary>
    /// 异步批量更新
    /// </summary>
    public async Task<bool> UpdateListAsync(List<T> t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.UpdateAsync<List<T>>(t);
      }
    }
 

    /// <summary>
    /// 异步删除
    /// </summary>
    public async Task<bool> DeleteAsync(T t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.DeleteAsync(t);
      }
    }

 

    /// <summary>
    /// 异步批量删除
    /// </summary>
    public async Task<bool> DeleteListAsync(List<T> t)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.DeleteAsync<List<T>>(t);
      }
    }

 

    /// <summary>
    /// 异步获取单条数据
    /// </summary>
    public async Task<T> SingleAsync(int id)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.GetAsync<T>(id);
      }
    }

 

    /// <summary>
    /// 根据SQL语句获取符合条件的所有对象
    /// </summary>
    public async Task<IEnumerable<TU>> FindAllAsync<TU>(string @sql, object param = null)
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.QueryAsync<TU>(sql, param);
      }
    }

 

    /// <summary>
    /// 获取所有表数据
    /// </summary>
    /// <typeparam name="T">泛型占位符</typeparam>
    public async Task<IEnumerable<T>> FindAllAsync<T>() where T : class
    {
      using (var dbConnection = Connection)
      {
        await dbConnection.OpenAsync();
        return await dbConnection.GetAllAsync<T>();
      }
    }

    /// <summary>
    /// 异步分页查询
    /// </summary>
    /// <param name="sql">sql字符串</param>
    /// <param name="param">参数</param>
    /// <param name="sortBy">排序（必须字段）</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页数量</param>
    /// <returns></returns>

   public async Task<PageData<T>> PagedAsync<T>(string sql, object param, string sortBy, long pageIndex, long pageSize)
    {
      await using var dbConnection = Connection;
      await dbConnection.OpenAsync();
      var pageResult = new PageData<T>();
      if (pageIndex < 1)
        pageIndex = 1;
      pageResult.PageIndex = pageIndex;
      pageResult.PageSize = pageSize;

      var queryNumSql = new StringBuilder($" SELECT COUNT(1) FROM ({sql}) AS mapTable ");
      pageResult.TotalCount = dbConnection.ExecuteScalar<long>(queryNumSql.ToString(), param);
      if (pageResult.TotalCount == 0 || pageSize == 0)
      {
        return pageResult;
      }
      var pageMinCount = (pageIndex - 1) * pageSize + 1;
      if (pageResult.TotalCount < pageMinCount)
      {
        pageIndex = (int)((pageResult.TotalCount - 1) / pageSize) + 1;
      }
      pageResult.TotalPages = pageResult.TotalCount / pageSize;
      if ((pageResult.TotalCount % pageSize) != 0)
        pageResult.TotalPages++;


      var querySql = new StringBuilder();
     if (!string.IsNullOrWhiteSpace(sortBy))
     {
       querySql.AppendLine(
         $@" SELECT  mapTable.*
             FROM   ( {sql}
                 ) AS mapTable
             ORDER BY {sortBy}
                 OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");
     }
     else
     {
       querySql.Append(
         $@" SELECT  b.*
             FROM   ( SELECT   ROW_NUMBER() OVER ( ORDER BY ( SELECT NULL ) ) rn ,
                       a.*  FROM    ( {sql}
                       ) a
                 ) b
             WHERE  b.rn > {(pageIndex - 1) * pageSize}
                 AND b.rn <= {pageSize * pageIndex} ");
     }

     pageResult.Items = dbConnection.Query<T>(querySql.ToString(), param).ToList();
     return pageResult;

   }

 
```

 