
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"CakeLogger",
        content:"CakeLogger",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"SchoolContext",
        content:"SchoolContext",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"IEfMigratorBackend",
        content:"IEfMigratorBackend",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"MigrationResult",
        content:"MigrationResult",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"AtomicMigrationScriptBuilder",
        content:"AtomicMigrationScriptBuilder",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"EfMigrationException",
        content:"EfMigrationException",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"EfMigratorSettings",
        content:"EfMigratorSettings",
        description:'',
        tags:''
    });

    a({
        id:11,
        title:"Student",
        content:"Student",
        description:'',
        tags:''
    });

    a({
        id:12,
        title:"EfAliases",
        content:"EfAliases",
        description:'',
        tags:''
    });

    a({
        id:13,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:14,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:15,
        title:"",
        content:"",
        description:'',
        tags:''
    });

    a({
        id:16,
        title:"Bad",
        content:"Bad",
        description:'',
        tags:''
    });

    a({
        id:17,
        title:"IEfMigrator",
        content:"IEfMigrator",
        description:'',
        tags:''
    });

    a({
        id:18,
        title:"Migration",
        content:"Migration",
        description:'',
        tags:''
    });

    a({
        id:19,
        title:"Class",
        content:"Class",
        description:'',
        tags:''
    });

    a({
        id:20,
        title:"EfMigrator",
        content:"EfMigrator",
        description:'',
        tags:''
    });

    a({
        id:21,
        title:"EfMigratorBackend",
        content:"EfMigratorBackend",
        description:'',
        tags:''
    });

    a({
        id:22,
        title:"ILogger",
        content:"ILogger",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V7',
        title:"V7",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.CakeTranslation/CakeLogger',
        title:"CakeLogger",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres/SchoolContext',
        title:"SchoolContext",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V5',
        title:"V5",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Interfaces/IEfMigratorBackend',
        title:"IEfMigratorBackend",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V0',
        title:"V0",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Models/MigrationResult',
        title:"MigrationResult",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V6',
        title:"V6",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres/AtomicMigrationScriptBuilder',
        title:"AtomicMigrationScriptBuilder",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Models/EfMigrationException',
        title:"EfMigrationException",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.CakeTranslation/EfMigratorSettings',
        title:"EfMigratorSettings",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Models/Student',
        title:"Student",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.CakeTranslation/EfAliases',
        title:"EfAliases",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V4',
        title:"V4",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V2',
        title:"V2",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/V3',
        title:"V3",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Migrations/Bad',
        title:"Bad",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Interfaces/IEfMigrator',
        title:"IEfMigrator",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Models/Migration',
        title:"Migration",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.TestProject.Postgres.Models/Class',
        title:"Class",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Migrator/EfMigrator',
        title:"EfMigrator",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Migrator/EfMigratorBackend',
        title:"EfMigratorBackend",
        description:""
    });

    y({
        url:'/Cake.EntityFramework/Cake.EntityFramework/api/Cake.EntityFramework.Interfaces/ILogger',
        title:"ILogger",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
