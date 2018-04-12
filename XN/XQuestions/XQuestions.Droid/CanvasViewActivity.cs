using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views;
using Android.Content;
using Android.Util;

namespace XQuestions.Droid
{
    [Activity(Label = "CanvasViewActivity")]
    public class CanvasViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // キャンバス
            CanvasView canvasView = new CanvasView(this);

            // 一番外側のレイアウト
            LinearLayout layoutAll = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical
            };
            // 入れ子の横レイアウト1個目
            LinearLayout layoutHorizontal1 = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };
            // 入れ子の横レイアウト2個目
            LinearLayout layoutHorizontal2 = new LinearLayout(this)
            {
                Orientation = Orientation.Horizontal
            };

            #region 1列目
            TextView textView1 = new TextView(this)
            {
                Text = "始点(X, Y): "
            };
            // 始点のX座標のEditTextを生成
            EditText editTextStartX = new EditText(this)
            {
                Hint = "Started X position",
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Weight = 1f
                }
            };
            // 始点のY座標のEditTextを生成
            EditText editTextStartY = new EditText(this)
            {
                Hint = "Started Y position",
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Weight = 1f
                }
            };

            // 1個目の入れ子に追加
            layoutHorizontal1.AddView(textView1);
            layoutHorizontal1.AddView(editTextStartX);
            layoutHorizontal1.AddView(editTextStartY);
            #endregion

            #region 2列目
            TextView textView2 = new TextView(this)
            {
                Text = "終点(X, Y): "
            };
            // 終点のX座標のEditTextを生成
            EditText editTextEndX = new EditText(this)
            {
                Hint = "Ended X position",
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Weight = 1f
                }
            };
            // 終点のY座標のEditTextを生成
            EditText editTextEndY = new EditText(this)
            {
                Hint = "Ended Y position",
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Weight = 1f
                }
            };

            // 2個目の入れ子に追加
            layoutHorizontal2.AddView(textView2);
            layoutHorizontal2.AddView(editTextEndX);
            layoutHorizontal2.AddView(editTextEndY);
            #endregion


            // Drawボタン生成
            Button drawButton = new Button(this)
            {
                Text = "Draw",
            };
            drawButton.Click += (sender, e) =>
            {
                float _startX = float.TryParse(editTextStartX.Text, out var sx) ? sx : 100;
                float _startY = float.TryParse(editTextStartY.Text, out var sy) ? sy : 100;
                float _endX = float.TryParse(editTextEndX.Text, out var ex) ? ex : 300;
                float _endY = float.TryParse(editTextEndY.Text, out var ey) ? ey : 300;

                canvasView.DrawLine(_startX, _startY, _endX, _endY);
            };

            // Clearボタン生成
            Button clearButton = new Button(this)
            {
                Text = "Clear",
            };
            clearButton.Click += (sender, e) =>
            {
                canvasView.ClearLine();
            };

            // 全体のレイアウトに入れ子のレイアウトとボタンを追加
            layoutAll.AddView(layoutHorizontal1);
            layoutAll.AddView(layoutHorizontal2);
            layoutAll.AddView(drawButton);
            layoutAll.AddView(clearButton);
            layoutAll.AddView(canvasView);

            SetContentView(layoutAll);
        }
    }

    public class CanvasView : View
    {
        private float _sx, _sy, _ex, _ey;
        private bool _flag;

        public CanvasView(Context context) : base(context)
        {

        }

        public void ClearLine()
        {
            _flag = false;
            Invalidate();
        }

        public void DrawLine(float startX, float startY, float endX, float endY)
        {
            _sx = startX;
            _sy = startY;
            _ex = endX;
            _ey = endY;

            _flag = true;
            Invalidate();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (_flag)
            {
                Paint paint = new Paint();
                paint.Color = Color.Argb(255, 255, 0, 0);
                paint.StrokeWidth = 20f;

                canvas.DrawLine(_sx, _sy, _ex, _ey, paint);
            }
            else
            {
                canvas.DrawColor(Color.Transparent, PorterDuff.Mode.Clear);
            }
        }
    }
}