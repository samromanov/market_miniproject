using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using market_miniproject.Classes;
using System;
using System.Collections.Generic;

namespace market_miniproject
{
    [Activity(Label = "AddNewTrack_Activity")]
    public class AddNewTrack_Activity : Activity
    {
        RadioGroup _addTrack_rdg;
        RadioButton _classical_rdb, _jazz_rdb, _rock_rdb, _other_rdb;
        ImageView _trackIcon, _editTrackIcon;
        Button _btnAttach, _btnAddTrack, _btnCancelAdding;
        EditText _title_input, _author_input, _duration_input, _price_input, _variable1_input, _variable2_input;

        int imageSelected_Id;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.addNewTrack);

            Init();

        }
        public void Init()
        {
            _addTrack_rdg = FindViewById<RadioGroup>(Resource.Id.addTrack_rdg);
            _classical_rdb = FindViewById<RadioButton>(Resource.Id.classical_rdb);
            _jazz_rdb = FindViewById<RadioButton>(Resource.Id.jazz_rdb);
            _rock_rdb = FindViewById<RadioButton>(Resource.Id.rock_rdb);
            _other_rdb = FindViewById<RadioButton>(Resource.Id.other_rdb);
            _trackIcon = FindViewById<ImageView>(Resource.Id.trackIcon);
            _editTrackIcon = FindViewById<ImageView>(Resource.Id.editTrackIcon);
            _btnAttach = FindViewById<Button>(Resource.Id.btnAttach);
            _btnAddTrack = FindViewById<Button>(Resource.Id.btnAddTrack);
            _title_input = FindViewById<EditText>(Resource.Id.title_input);
            _author_input = FindViewById<EditText>(Resource.Id.author_input);
            _duration_input = FindViewById<EditText>(Resource.Id.duration_input);
            _price_input = FindViewById<EditText>(Resource.Id.price_input);
            _variable1_input = FindViewById<EditText>(Resource.Id.variable1_input);
            _variable2_input = FindViewById<EditText>(Resource.Id.variable2_input);
            _btnCancelAdding = FindViewById<Button>(Resource.Id.btnCancelAdding);

            _classical_rdb.Click += _classical_rdb_Click;
            _jazz_rdb.Click += _jazz_rdb_Click;
            _rock_rdb.Click += _rock_rdb_Click;
            _other_rdb.Click += _other_rdb_Click;

            _editTrackIcon.Click += _editTrackIcon_Click;

            //_btnAttach.Click += _btnAttach_Click; // ***must work on it***

            _btnAddTrack.Click -= _btnAddTrack_Click;
            _btnAddTrack.Click += _btnAddTrack_Click;

            _btnCancelAdding.Click += _btnCancelAdding_Click;

            //------------------------------------------------------------------------
            imageSelected_Id = Resource.Drawable.classics_icon;
            _classical_rdb.Checked = true; // Initially selected on "classical"
            _jazz_rdb.Checked = false;
            _rock_rdb.Checked = false;
            _other_rdb.Checked = false;

            _trackIcon.SetBackgroundResource(Resource.Drawable.classics_icon);
            _title_input.Hint = "Piece title";
            _author_input.Hint = "Composer";
            _variable1_input.Hint = "Era";
            _variable2_input.Hint = "Type";
            _variable2_input.InputType = Android.Text.InputTypes.ClassText;
            _variable2_input.Visibility = ViewStates.Visible;
        }

        private void _btnCancelAdding_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void _btnAddTrack_Click(object sender, EventArgs e)
        {
            if (_other_rdb.Checked == true)
            {
                if (_author_input.Text == "" || _title_input.Text == "" || _duration_input.Text == "" || _price_input.Text == "" || _variable1_input.Text == "") // check empty fields (other track)
                {
                    Toast.MakeText(this, "Empty fields detected!", ToastLength.Short).Show();
                }
                else
                {
                    string title = _title_input.Text;
                    string author = _author_input.Text;
                    int duration = int.Parse(_duration_input.Text);
                    double price = double.Parse(_price_input.Text);
                    string genre = _variable1_input.Text;

                    OtherTrack newTrack = new OtherTrack(imageSelected_Id, title, author, duration, price, genre);
                    bool exists = false;
                    foreach (var item in ProductsList.productsList)
                    {
                        if (newTrack == item)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (exists == true)
                    {
                        Toast.MakeText(this, $"{newTrack.TrackTitle} already exists!", ToastLength.Short).Show();
                    }
                    else // if (exists == false)
                    {
                        ProductsList.productsList.Add(newTrack);
                        Toast.MakeText(this, $"{newTrack.TrackTitle} successfully added!", ToastLength.Short).Show();
                        Finish();
                    }
                }
            }
            else
            {
                if (_author_input.Text == "" || _title_input.Text == "" || _duration_input.Text == "" || _price_input.Text == "" || _variable1_input.Text == "" || _variable2_input.Text == "") // check empty fields (general)
                {
                    Toast.MakeText(this, "Empty fields detected!", ToastLength.Short).Show();
                }
                else
                {
                    if (_classical_rdb.Checked == true)
                    {
                        string pieceTitle = _title_input.Text;
                        string composer = _author_input.Text;
                        int duration = int.Parse(_duration_input.Text);
                        double price = double.Parse(_price_input.Text);
                        string era = _variable1_input.Text;
                        string type = _variable2_input.Text;

                        ClassicalTrack newClassicalTrack = new ClassicalTrack(imageSelected_Id, pieceTitle, composer, duration, price, era, type);

                        bool exists = false;
                        foreach (var item in ProductsList.productsList)
                        {
                            if (newClassicalTrack == item)
                        {
                                exists = true;
                                break;
                            }
                        }
                        if (exists == true)
                        {
                            Toast.MakeText(this, $"{newClassicalTrack.TrackTitle} already exists!", ToastLength.Short).Show();
                        }
                        else // if (exists == false)
                        {
                            ProductsList.productsList.Add(newClassicalTrack);
                            Toast.MakeText(this, $"{newClassicalTrack.TrackTitle} successfully added!", ToastLength.Short).Show();
                            Finish();
                        }
                    }
                    else if (_jazz_rdb.Checked == true)
                    {
                        string pieceTitle = _title_input.Text;
                        string author = _author_input.Text;
                        int duration = int.Parse(_duration_input.Text);
                        double price = double.Parse(_price_input.Text);
                        string leadInstrument = _variable1_input.Text;
                        int soloDuration = int.Parse(_variable2_input.Text);

                        JazzTrack newJazzTrack = new JazzTrack(imageSelected_Id, pieceTitle, author, duration, price, leadInstrument, soloDuration);

                        bool exists = false;
                        foreach (var item in ProductsList.productsList)
                        {
                            if (newJazzTrack == item)
                        {
                                exists = true;
                                break;
                            }
                        }
                        if (exists == true)
                        {
                            Toast.MakeText(this, $"{newJazzTrack.TrackTitle} already exists!", ToastLength.Short).Show();
                        }
                        else // if (exists == false)
                        {
                            ProductsList.productsList.Add(newJazzTrack);
                            Toast.MakeText(this, $"{newJazzTrack.TrackTitle} successfully added!", ToastLength.Short).Show();
                            Finish();
                        }
                    }
                    else // if (_rock_rdb.Checked == true)
                    {
                        string songName = _title_input.Text;
                        string bandName = _author_input.Text;
                        int duration = int.Parse(_duration_input.Text);
                        double price = double.Parse(_price_input.Text);
                        string subGenre = _variable1_input.Text;
                        string lyricsTheme = _variable2_input.Text;

                        RockTrack newRockTrack = new RockTrack(imageSelected_Id, songName, bandName, duration, price, subGenre, lyricsTheme);

                        bool exists = false;
                        foreach (var item in ProductsList.productsList)
                        {
                            if (newRockTrack == item)
                        {
                                exists = true;
                                break;
                            }
                        }
                        if (exists == true)
                        {
                            Toast.MakeText(this, $"{newRockTrack.TrackTitle} already exists!", ToastLength.Short).Show();
                        }
                        else // if (exists == false)
                        {
                            ProductsList.productsList.Add(newRockTrack);
                            Toast.MakeText(this, $"{newRockTrack.TrackTitle} successfully added!", ToastLength.Short).Show();
                            Finish();
                        }
                    }
                }
            }            
        }

        private void _btnAttach_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _editTrackIcon_Click(object sender, EventArgs e)
        {
            var editIcon_dialog = new Dialog(this);
            editIcon_dialog.SetContentView(Resource.Layout.editIcon_sample);
            var edit_op1 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op1);
            var edit_op2 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op2);
            var edit_op3 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op3);
            var edit_op4 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op4);
            var edit_op5 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op5);
            var edit_op6 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op6);
            var edit_op7 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op7);
            var edit_op8 = editIcon_dialog.FindViewById<ImageButton>(Resource.Id.edit_op8);

            edit_op1.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.classics_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op2.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.rock_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op3.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.jazz_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op4.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.other1_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op5.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.other2_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op6.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.other3_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op7.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.other4_icon);
                editIcon_dialog.Dismiss();
            };
            edit_op8.Click += (sender, e) =>
            {
                ChangeIconFunction(Resource.Drawable.other5_icon);
                editIcon_dialog.Dismiss();
            };

            editIcon_dialog.Show();
        }
        private void ChangeIconFunction(int imgId)
        {
            imageSelected_Id = imgId;
            _trackIcon.SetBackgroundResource(imgId);
        }

        

        private void _other_rdb_Click(object sender, EventArgs e)
        {
            imageSelected_Id = Resource.Drawable.other1_icon;
            _classical_rdb.Checked = false;
            _jazz_rdb.Checked = false;
            _rock_rdb.Checked = false;

            _trackIcon.SetBackgroundResource(Resource.Drawable.other1_icon);
            _title_input.Hint = "Title";
            _author_input.Hint = "Author";
            _variable1_input.Hint = "Genre";
            _variable2_input.Visibility = ViewStates.Gone;
        }

        private void _rock_rdb_Click(object sender, EventArgs e)
        {
            imageSelected_Id = Resource.Drawable.rock_icon;
            _classical_rdb.Checked = false;
            _jazz_rdb.Checked = false;
            _other_rdb.Checked = false;

            _trackIcon.SetBackgroundResource(Resource.Drawable.rock_icon);
            _title_input.Hint = "Song name";
            _author_input.Hint = "Band or author";
            _variable1_input.Hint = "Sub genre";
            _variable2_input.Hint = "Lyrics theme";
            _variable2_input.InputType = Android.Text.InputTypes.ClassText;
            _variable2_input.Visibility = ViewStates.Visible;
        }

        private void _jazz_rdb_Click(object sender, EventArgs e)
        {
            imageSelected_Id = Resource.Drawable.jazz_icon;
            _classical_rdb.Checked = false;
            _rock_rdb.Checked = false;
            _other_rdb.Checked = false;

            _trackIcon.SetBackgroundResource(Resource.Drawable.jazz_icon);
            _title_input.Hint = "Piece title";
            _author_input.Hint = "Author";
            _variable1_input.Hint = "Lead instrument";
            _variable2_input.Hint = "Solo duration (s)";
            _variable2_input.InputType = Android.Text.InputTypes.ClassNumber;
            _variable2_input.Visibility = ViewStates.Visible;
        }

        private void _classical_rdb_Click(object sender, EventArgs e)
        {
            imageSelected_Id = Resource.Drawable.classics_icon;
            _jazz_rdb.Checked = false;
            _rock_rdb.Checked = false;
            _other_rdb.Checked = false;

            _trackIcon.SetBackgroundResource(Resource.Drawable.classics_icon);
            _title_input.Hint = "Piece title";
            _author_input.Hint = "Composer";
            _variable1_input.Hint = "Era";
            _variable2_input.Hint = "Type";
            _variable2_input.InputType = Android.Text.InputTypes.ClassText;
            _variable2_input.Visibility = ViewStates.Visible;

        }
    }
}