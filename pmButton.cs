﻿using paper_maze;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace paper_maze
{
    public class pmButton : Button
    {
        #region -- Свойства --

        [Description("Цвет обводки (границы) кнопки")]
        public Color BorderColor { get; set; } = Color.Tomato;

        [Description("Указывает, включено ли использование отдельного цвета обводки (границы) кнопки")]
        public bool BorderColorEnabled { get; set; } = false;

        [Description("Цвет обводки (границы) кнопки при наведении курсора")]
        public Color BorderColorOnHover { get; set; } = Color.Tomato;

        [Description("Указывает, включено ли использование отдельного цвета обводки (границы) кнопки при наведении курсора")]
        public bool BorderColorOnHoverEnabled { get; set; } = false;

        [Description("Дополнительный фоновый цвет кнопки используемый для создания градиента (При BackColorGradientEnabled = true)")]
        public Color BackColorAdditional { get; set; } = Color.Gray;

        [Description("Указывает, включен ли градинт кнопки")]
        public bool BackColorGradientEnabled { get; set; } = false;

        [Description("Определяет направление линейного градиента шапки")]
        public LinearGradientMode BackColorGradientMode { get; set; } = LinearGradientMode.Horizontal;

        [Description("Текст, отображаемый при наведении курсора")]
        public string TextHover { get; set; }

        private bool roundingEnable = false;
        [Description("Вкл/Выкл закругление объекта")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус закругления объекта в процентном соотношении")]
        public int Rounding
        {
            get => roundingPercent;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    roundingPercent = value;

                    Refresh();
                }
            }
        }

        [Description("Вкл/Выкл эффект волны по нажатию кнопки курсором.")]
        public bool UseRippleEffect { get; set; } = true;

        [Description("Цвет эффекта волны по нажатию кнопки курсором")]
        public Color RippleColor { get; set; } = Color.Black;

        [Description("Вкл/Выкл эффект нажатия кнопки.")]
        public bool UseDownPressEffectOnClick { get; set; }

        public bool UseZoomEffectOnHover { get; set; }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        #endregion

        #region -- Переменные --

        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;

        Animation CurtainButtonAnim = new Animation();
 
        Animation TextSlideAnim = new Animation();

        Dictionary<Animation, Rectangle> RippleButtonAnimDic = new Dictionary<Animation, Rectangle>();

        Point ClickLocation = new Point();

        #endregion

        public pmButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint |
                ControlStyles.Opaque |
                ControlStyles.Selectable |
                ControlStyles.UserMouse |
                ControlStyles.EnableNotifyMessage,
                true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            Font = new Font("Verdana", 8.25F, FontStyle.Regular);

            Cursor = Cursors.Hand;

            BackColor = Color.Tomato;
            BorderColor = BackColor;
            ForeColor = Color.White;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectCurtain = new Rectangle(0, 0, (int)CurtainButtonAnim.Value, Height - 1);

            Rectangle rectText = new Rectangle((int)TextSlideAnim.Value, rect.Y, rect.Width, rect.Height);
            Rectangle rectTextHover = new Rectangle((int)TextSlideAnim.Value - rect.Width, rect.Y, rect.Width, rect.Height);

            float roundingValue = 0.1F;
            if (RoundingEnable && roundingPercent > 0)
            {
                roundingValue = Height / 100F * roundingPercent;
            }
            GraphicsPath rectPath = uiDrawer.RoundedRectangle(rect, roundingValue);

            Region = new Region(rectPath);
            graph.Clear(Parent.BackColor);


            Brush headerBrush = new SolidBrush(BackColor);
            if (BackColorGradientEnabled)
            {
                if (rect.Width > 0 && rect.Height > 0)
                    headerBrush = new LinearGradientBrush(rect, BackColor, BackColorAdditional, BackColorGradientMode);
            }

            Brush borderBrush = headerBrush;
            if (BorderColorEnabled)
            {
                borderBrush = new SolidBrush(BorderColor);

                if (MouseEntered && BorderColorOnHoverEnabled)
                    borderBrush = new SolidBrush(BorderColorOnHover);
            }

            graph.DrawPath(new Pen(borderBrush), rectPath);
            graph.FillPath(headerBrush, rectPath);

            graph.SetClip(rectPath);

            graph.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rectCurtain);
            graph.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rectCurtain);


            if (UseRippleEffect == false)
            {
                if (MousePressed)
                {
                    graph.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rect);
                    graph.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
                }
            }
            else
            {
                for (int i = 0; i < RippleButtonAnimDic.Count; i++)
                {
                    KeyValuePair<Animation, Rectangle> animRect = RippleButtonAnimDic.ToList()[i];
                    Animation MultiRippleButtonAnim = animRect.Key;
                    Rectangle rectMultiRipple = animRect.Value;

                    rectMultiRipple = new Rectangle(
                       ClickLocation.X - (int)MultiRippleButtonAnim.Value / 2,
                       ClickLocation.Y - (int)MultiRippleButtonAnim.Value / 2,
                       (int)MultiRippleButtonAnim.Value,
                       (int)MultiRippleButtonAnim.Value
                       );

                    if (MultiRippleButtonAnim.Value > 0 && MultiRippleButtonAnim.Value < MultiRippleButtonAnim.TargetValue)
                    {
                        graph.DrawEllipse(new Pen(Color.FromArgb(30, RippleColor)), rectMultiRipple);
                        graph.FillEllipse(new SolidBrush(Color.FromArgb(30, RippleColor)), rectMultiRipple);
                    }
                    else if (MultiRippleButtonAnim.Value == MultiRippleButtonAnim.TargetValue)
                    {
                        if (MousePressed == false)
                        {
                            MultiRippleButtonAnim.Value = 0;
                            MultiRippleButtonAnim.Status = Animation.AnimationStatus.Completed;
                        }
                        else
                        {
                            if (i == RippleButtonAnimDic.Count - 1)
                            {
                                graph.DrawEllipse(new Pen(Color.FromArgb(30, RippleColor)), rectMultiRipple);
                                graph.FillEllipse(new SolidBrush(Color.FromArgb(30, RippleColor)), rectMultiRipple);
                            }
                        }
                    }
                }
                List<Animation> completedRippleAnimations = RippleButtonAnimDic.Keys.ToList().FindAll(x => x.Status == Animation.AnimationStatus.Completed);
                for (int i = 0; i < completedRippleAnimations.Count; i++)
                    RippleButtonAnimDic.Remove(completedRippleAnimations[i]);
            }

            if (string.IsNullOrEmpty(TextHover))
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
            }
            else
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), rectText, SF);
                graph.DrawString(TextHover, Font, new SolidBrush(ForeColor), rectTextHover, SF);
            }
        }

        private void TextSlideAction()
        {
            if (MouseEntered)
            {
                TextSlideAnim = new Animation("TextSlide_" + Handle, Invalidate, TextSlideAnim.Value, Width - 1);
            }
            else
            {
                TextSlideAnim = new Animation("TextSlide_" + Handle, Invalidate, TextSlideAnim.Value, 0);
            }

            TextSlideAnim.StepDivider = 8;
            Animator.Request(TextSlideAnim, true);
        }

        private void ButtonMultiRippleAction()
        {
            Animation MultiRippleButtonAnim = new Animation("ButtonMultiRipple_" + Handle + DateTime.Now.Millisecond, Invalidate, 0, Width * 3);
            MultiRippleButtonAnim.StepDivider = 20;

            Animator.Request(MultiRippleButtonAnim);

            RippleButtonAnimDic.Add(MultiRippleButtonAnim, new Rectangle());
        }

        private void ButtonCurtainAction()
        {
            if (MouseEntered)
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, Width - 1);
            }
            else
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, 0);
            }

            CurtainButtonAnim.StepDivider = 8;
            Animator.Request(CurtainButtonAnim, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            if (UseZoomEffectOnHover)
            {
                Rectangle buttonRect = new Rectangle(Location, Size);
                buttonRect.Inflate(1, 1);
                Location = buttonRect.Location;
                Size = buttonRect.Size;
            }

            ButtonCurtainAction();
            TextSlideAction();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            if (UseZoomEffectOnHover)
            {
                Rectangle buttonRect = new Rectangle(Location, Size);
                buttonRect.Inflate(-1, -1);
                Location = buttonRect.Location;
                Size = buttonRect.Size;
            }

            ButtonCurtainAction();
            TextSlideAction();

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            CurtainButtonAnim.Value = CurtainButtonAnim.TargetValue;

            ClickLocation = e.Location;
            //ButtonRippleAction();
            ButtonMultiRippleAction();

            if (UseDownPressEffectOnClick) Location = new Point(Location.X, Location.Y + 2);

            Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();

            if (UseDownPressEffectOnClick) Location = new Point(Location.X, Location.Y - 2);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }
}
