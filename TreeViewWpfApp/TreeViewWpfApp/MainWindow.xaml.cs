using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace TreeViewWpfApp
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Teacher> teachers = new List<Teacher>(); //所有老師的List
        List<Record> records = new List<Record>(); //所有選課紀錄的List
        List<Student> students = new List<Student>(); //所有學生的List
        List<Course> courses = new List<Course>(); //所有課程了List
        Teacher selectedTeacher = null; //選取的老師
        Course selectedCourse = null; //選取的課程
        Student selectedStudent = null; //選取的學生
        Record selectedRecord = null; //選取的選課紀錄
        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show($"歡迎使用選課系統。\n請先選擇檔案。");

            Teacher teacher1 = new Teacher() { TeacherName = "陳定宏" };
            teacher1.Courses.Add(new Course(teacher1) { CourseName = "視窗程式設計", Type = "選修", Point = 3, OpeningClass = "五專資工三甲" });
            teacher1.Courses.Add(new Course(teacher1) { CourseName = "視窗程式設計", Type = "選修", Point = 3, OpeningClass = "四技資工二甲" });
            teacher1.Courses.Add(new Course(teacher1) { CourseName = "視窗程式設計", Type = "選修", Point = 3, OpeningClass = "四技資工二乙" });
            teachers.Add(teacher1);

            Teacher teacher2 = new Teacher() { TeacherName = "陳福坤" };
            teacher2.Courses.Add(new Course(teacher2) { CourseName = "計算機概論", Type = "必修", Point = 3, OpeningClass = "四技資工一丙" });
            teacher2.Courses.Add(new Course(teacher2) { CourseName = "數位系統導論(A)", Type = "管制必修", Point = 3, OpeningClass = "四技資工一甲等合開" });
            teacher2.Courses.Add(new Course(teacher2) { CourseName = "視窗程式設計", Type = "管制必修", Point = 3, OpeningClass = "四技資工一甲等合開" });
            teachers.Add(teacher2);

            Teacher teacher3 = new Teacher() { TeacherName = "林泓宏" };
            teacher3.Courses.Add(new Course(teacher3) { CourseName = "計算機概論", Type = "必修", Point = 3, OpeningClass = "四技資工一乙" });
            teacher3.Courses.Add(new Course(teacher3) { CourseName = "演算法", Type = "必修", Point = 4, OpeningClass = "五專資工四甲" });
            teacher3.Courses.Add(new Course(teacher3) { CourseName = "人工智慧概論", Type = "必修", Point = 3, OpeningClass = "五專資工三甲" });
            teacher3.Courses.Add(new Course(teacher3) { CourseName = "資料結構", Type = "必修", Point = 2, OpeningClass = "五專資二甲" });
            teachers.Add(teacher3);

            treTeacher.ItemsSource = teachers;

            //將教師所授課的課程讀入courses內
            foreach (Teacher teacher in teachers)
            {
                foreach (Course course in teacher.Courses)
                {
                    courses.Add(course);
                }
            }

            lbCourse.ItemsSource = courses;


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "讀取學生資料";
            openFileDialog.Filter = "Json檔案|*.json|所有檔案|*.*";//設定開啟檔案類性
            openFileDialog.DefaultExt = "*.json";//預設類型
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;//檔案路徑
                //System.Windows.MessageBox.Show(path);
                String json = File.ReadAllText(path);
                students = JsonConvert.DeserializeObject<List<Student>>(json);

            }

            /*Student student1 = new Student()
            {
                StudentID = "A123456789",
                StudentName = "陳小明"
            };
            students.Add(student1);

            Student student2 = new Student()
            {
                StudentID = "B00000000",
                StudentName = "陳大明"
            };
            students.Add(student2);*/

            cmbStudent.ItemsSource = students;
        }

        private void treTeacher_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treTeacher.SelectedItem is Course)
            {
                selectedCourse = treTeacher.SelectedItem as Course;
                selectedTeacher = selectedCourse.Tutor;

                //MessageBox.Show(selectedTeacher.ToString() + selectedCourse.ToString());
                statusLabel.Content = selectedTeacher.ToString() + "    " + selectedCourse.ToString();
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null && selectedCourse != null)
            {
                Record currentRecord = new Record()
                {
                    SelectedStudent = selectedStudent,
                    TeacherName = selectedTeacher.TeacherName,
                    SelectedCourse = selectedCourse
                };
                foreach (Record r in records)
                {
                    if (r.Equals(currentRecord))
                    {
                        MessageBox.Show($"{selectedStudent.StudentName}已經選過{selectedCourse.CourseName}了，請重新選擇未選過的課程。");
                        return;
                    }
                }
                records.Add(currentRecord);
                lvRecord.ItemsSource = records;
                lvRecord.Items.Refresh();

            }
            else MessageBox.Show("請先選學生或課程");
        }
        private void cmbStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStudent = cmbStudent.SelectedItem as Student;
            statusLabel.Content = selectedStudent.ToString();
        }

        private void lbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCourse = lbCourse.SelectedItem as Course;
            selectedTeacher = selectedCourse.Tutor;
            statusLabel.Content = selectedCourse.ToString();
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecord != null)
            {
                records.Remove(selectedRecord);
                lvRecord.ItemsSource = records;
                lvRecord.Items.Refresh();
            }
            else MessageBox.Show("請選要退選的選課紀錄");
        }

        private void lvRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecord = lvRecord.SelectedItem as Record;
            //statusLabel.Content = selectedRecord.ToString();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "儲存選課紀錄";
            saveFileDialog.Filter = "Json檔案|*.json|所有檔案|*.*";//設定開啟檔案類性
            saveFileDialog.DefaultExt = "*.json";//預設類型

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serializer.Formatting = Formatting.Indented;
                // string jsonString = System.Text.Json.JsonSerializer.Serialize(records);
                //System.Windows.MessageBox.Show(jsonString);
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, records);
                }

            }
        }
    }
    public class Course
    {
        public string CourseName { get; set; }
        public string Type { get; set; }
        public int Point { get; set; }
        public string OpeningClass { get; set; }
        public Teacher Tutor { get; set; }
        public Course(Teacher tutor)
        {
            this.Tutor = tutor;
        }
        public override string ToString()
        {
            return $"課程名稱 : {CourseName} {Type} {Point}學分 開課班級:{OpeningClass}";
        }
    }

    public class Teacher
    {
        public string TeacherName { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public Teacher()
        {
            this.Courses = new ObservableCollection<Course>();
        }
        public override string ToString()
        {
            return $"{TeacherName}";
        }
    }

    public class Student
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public override string ToString()
        {
            return $"{StudentID} {StudentName}";
        }
    }

    public class Record
    {
        public string TeacherName { get; set; }
        public Student SelectedStudent { get; set; }
        public Course SelectedCourse { get; set; }
        public bool Equals(Record r)
        {
            if (this.SelectedStudent.StudentID == r.SelectedStudent.StudentID && this.SelectedCourse.CourseName == r.SelectedCourse.CourseName) return true;
            else return false;
        }
        public override string ToString()
        {
            return $"{SelectedStudent}選課紀錄 --- {SelectedCourse}";
        }
    }
}