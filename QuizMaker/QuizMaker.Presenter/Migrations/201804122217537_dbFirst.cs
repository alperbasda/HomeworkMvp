namespace QuizMaker.Presenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerClassics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyAnswer = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        Highest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lowest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Avarage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyQuestion = c.String(),
                        Points = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        Difficulty = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.QuestionExamRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LessonId = c.Int(nullable: false),
                        Observer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LessonName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnswerTrueFalseTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyAnswer = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        AnswerCount = c.Int(nullable: false),
                        TrueAnswerCount = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerTrueFalseTests", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerClassics", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionExamRelations", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionExamRelations", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Questions", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Exams", "LessonId", "dbo.Lessons");
            DropIndex("dbo.AnswerTrueFalseTests", new[] { "QuestionId" });
            DropIndex("dbo.Exams", new[] { "LessonId" });
            DropIndex("dbo.QuestionExamRelations", new[] { "ExamId" });
            DropIndex("dbo.QuestionExamRelations", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "LessonId" });
            DropIndex("dbo.AnswerClassics", new[] { "QuestionId" });
            DropTable("dbo.AnswerTrueFalseTests");
            DropTable("dbo.Lessons");
            DropTable("dbo.Exams");
            DropTable("dbo.QuestionExamRelations");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerClassics");
        }
    }
}
