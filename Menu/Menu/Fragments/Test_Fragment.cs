using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Menu.Fragments
{
    public class Test_Fragment : Fragment
    {
        int question_number=1;
        int score = 0;
        View view;
        string answer;
        RadioButton option1, option2, option3, option4;
        RadioGroup options;TextView question, QN, QN_score;
        TextView TimerText;
        int _countSeconds;
        System.Timers.Timer _timer;
        bool timeOut = false;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.Test_Layout, container, false);
            options = view.FindViewById<RadioGroup>(Resource.Id.options);
            option1 = view.FindViewById<RadioButton>(Resource.Id.option1);
            option2 = view.FindViewById<RadioButton>(Resource.Id.option2);
            option3 = view.FindViewById<RadioButton>(Resource.Id.option3);
            option4 = view.FindViewById<RadioButton>(Resource.Id.option4);
            TimerText = view.FindViewById<TextView>(Resource.Id.Timer);
            var submit = view.FindViewById<Button>(Resource.Id.button1);
            question = view.FindViewById<TextView>(Resource.Id.textView1);
            QN = view.FindViewById<TextView>(Resource.Id.QN);
            QN_score = view.FindViewById<TextView>(Resource.Id.score);
            QN_score.Text = "Score:"+score.ToString();
            QN.Text = "Question:"+question_number.ToString();

            question.Text = GetString(Resource.String.question_1);
            var line11 = GetString(Resource.String.q1_option_1).ToCharArray();
            option1.SetText(line11, 0, line11.Length);
            var line12 = GetString(Resource.String.q1_option_2).ToCharArray();
            option2.SetText(line12, 0, line12.Length);
            var line13 = GetString(Resource.String.q1_option_3).ToCharArray();
            option3.SetText(line13, 0, line13.Length);
            var line14 = GetString(Resource.String.q1_option_4).ToCharArray();
            option4.SetText(line14, 0, line14.Length);
            Quiz();
            submit.Click += delegate
            {
                reset();
                if(question_number < 3)
                {
                    Quiz();
                    var optionclicked = view.FindViewById<RadioButton>(options.CheckedRadioButtonId).Text;
                    evaluate(optionclicked, question_number);
                    question_number++;
                    nextquestion(question_number);
                    
                }
                else
                {
                    TimerText.Text = "00:00s";
                }
                
            };
                
            return view;
        }
        void evaluate(string ans,int qn)
        {
            switch(qn)
            {
                case 1:
                    answer = GetString(Resource.String.q1_answer);
                    if (answer == ans)
                    {
                        score++;
                    }
                    break;
                case 2:
                    answer = GetString(Resource.String.q2_answer);
                    if (answer == ans)
                    {
                        score++;
                    }
                    break;
                case 3:
                    answer = GetString(Resource.String.q3_answer);
                    if (answer == ans)
                    {
                        score++;
                    }
                    break;
            }
            Console.WriteLine("Question no:",qn);
            Console.WriteLine("Answer selected : ",ans);
            Console.WriteLine("Correct answer: ",answer);

        }
        void nextquestion(int qn)
        {
            QN.Text = "Question:"+question_number.ToString();
            QN_score.Text = "Score:" + score.ToString();
            switch (qn)
            {
                case 2:
                    question.Text = GetString(Resource.String.question_2);
                    var line21 = GetString(Resource.String.q2_option_1).ToCharArray();
                    option1.SetText(line21, 0, line21.Length);
                    var line22 = GetString(Resource.String.q2_option_2).ToCharArray();
                    option2.SetText(line22, 0, line22.Length);
                    var line23 = GetString(Resource.String.q2_option_3).ToCharArray();
                    option3.SetText(line23, 0, line23.Length);
                    var line24 = GetString(Resource.String.q2_option_4).ToCharArray();
                    option4.SetText(line24, 0, line24.Length);
                    break;

                case 3:
                    question.Text = GetString(Resource.String.question_3);
                    var line31 = GetString(Resource.String.q3_option_1).ToCharArray();
                    option1.SetText(line31, 0, line31.Length);
                    var line32 = GetString(Resource.String.q3_option_2).ToCharArray();
                    option2.SetText(line32, 0, line32.Length);
                    var line33 = GetString(Resource.String.q3_option_3).ToCharArray();
                    option3.SetText(line33, 0, line33.Length);
                    var line34 = GetString(Resource.String.q3_option_4).ToCharArray();
                    option4.SetText(line34, 0, line34.Length);
                    break;
            }
        }
        private void Quiz()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;            
            _countSeconds = 30;
            _timer.Enabled = true;
            if (!timeOut)
            {
                TimerText.Text = "00:00s";// questionOption();
            }
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _countSeconds--;

            updateTimer(_countSeconds);

            if (_countSeconds == 0)
            {
                reset();
            }
        }

        public void reset()
        {
            this.Activity.RunOnUiThread(() => {
                TimerText.Text = "00:00s";
                //close.Visibility = Android.Views.ViewStates.Visible;
                //replay.Visibility = Android.Views.ViewStates.Visible;
                //done.Visibility = Android.Views.ViewStates.Visible;
                timeOut = true;
            });
            _timer.Stop();
        }

        public void updateTimer(int secondsLeft)
        {
            string secondsTime = secondsLeft.ToString();
            if (secondsLeft <= 9)
            {
                secondsTime = "0" + secondsLeft.ToString();
            }
            this.Activity.RunOnUiThread(() => {
                
                TimerText.Text = "00:" + secondsTime + "s";
            });
        }


    }
}