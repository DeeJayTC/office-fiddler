using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace OfficeFiddleMVC.Models
{
  public class MainData : DbContext
  {
    public MainData() : base("DefaultConnection")
    {
      Database.SetInitializer<MainData>(new CreateDatabaseIfNotExists<MainData>());
    }
    public DbSet<Fiddle> Fiddles { get; set; }
    public DbSet<Category> Categories { get; set; }



  }

  public class MainDataInitializer : CreateDatabaseIfNotExists<MainData>
  {
    protected override void Seed(MainData context)
    {
      var defaultFiddle = new Fiddle()
      {
        Application = "EXCEL",
        CSS = @".padding {
                 color: red;
                }",
        HTML = @"<div id='content - header'>
         <div class='padding'>
               <h1>Welcome to the Example</h1>
          </div>
          </div>
    <div id = 'content-main' >
           <div class='padding'>
               <button id='SetValue'> Set Value</button>
           </div>
    </div>",
        id = 0,
        IsPublic = true,
        JS = @"$(document).ready(function () {
                $('#SetValue').click(function() {Excel.run(function (ctx) {var activesheet = ctx.workbook.worksheets.getActiveWorksheet();
                     var range = activesheet.getRange('A1:C3');
                     range.values = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
                     return ctx.sync();
                     }).catch(function(error){
                      console.log(error); 
                            });
                              });
                  });",
        Name = "Add Values Default Fiddle",
        userid = "SYSTEM"
      };

      context.Fiddles.Add(defaultFiddle);

      base.Seed(context);
    }
  }


  public class ApplicationInfo
  {
    public string Application;
    public string VersionKey;
    public string Version;
  }


  public class Fiddle
  {
    [Key]
    public int id { get; set; }
    public string Name { get; set; }
    public string HTML { get; set; }
    public string CSS { get; set; }
    public string JS { get; set; }
    public string userid { get; set; }
    public bool IsPublic { get; set; }
    public string Application { get; set; }
    public string Version { get; set; }

    public string Description { get; set; }
    public virtual Category Category { get; set; }

    [NotMapped]
    public int CatID { get; set; }

  }


  public class Category
  {
    [Key]
    public int id { get; set; }
    public string Name { get; set; }
  }


}