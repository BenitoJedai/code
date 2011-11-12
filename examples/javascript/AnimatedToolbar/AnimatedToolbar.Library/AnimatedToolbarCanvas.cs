using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Input;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.Extensions;


namespace AnimatedToolbar.Library
{
    public class AnimatedToolbarCanvas : Canvas
    {
        public class AnimatedToolbarItem
        {
            public object Tag;

            public Image Image;

            public static implicit operator AnimatedToolbarItem(Image e)
            {
                return new AnimatedToolbarItem { Image = e };
            }

            #region internal
            internal Canvas Button;
            internal int x;
            internal int cx;
            internal AnimatedOpacity<Canvas> a;

            internal Action MoveTo;
            #endregion
        }

        public BindingList<AnimatedToolbarItem> Items;

        public event Action<AnimatedToolbarItem> ItemClicked;

        public int MaxItems = 4;

        public AnimatedToolbarItem SelectedItem;

        public AnimatedToolbarCanvas()
        {
            var ButtonOuterWidth = 16 + 4;

            Items = new BindingList<AnimatedToolbarItem>();
            Items.WithEvents(
                (AddedSource, AddedIndex) =>
                {
                    AddedSource.x = AddedIndex * ButtonOuterWidth;
                    AddedSource.cx = 8;

                    AddedSource.Button = new Canvas
                    {
                        Cursor = Cursors.Hand
                    }.AttachTo(this);

                    AddedSource.MoveTo =
                        delegate
                        {
                            AddedSource.Button.MoveTo(AddedSource.x + AddedSource.cx, 0);
                        };

                    #region ItemClicked
                    AddedSource.Button.MouseLeftButtonUp +=
                        delegate
                        {
                            if (ItemClicked != null)
                                ItemClicked(AddedSource);
                        };
                    #endregion

                    AddedSource.Button.MouseEnter +=
                        delegate
                        {
                            SelectedItem = AddedSource;
                        };

                    AddedSource.a = AddedSource.Button.ToAnimatedOpacity();
                    AddedSource.a.Opacity = 0;

                    #region fade in and slide left
                    AddedSource.Image.AttachTo(AddedSource.Button);

                    AddedSource.a.Opacity = 1;

                    if (AddedSource.cx > 0)
                        (1000 / 60).AtIntervalWithTimerAndCounter(
                            (t, c) =>
                            {
                                AddedSource.cx--;

                                AddedSource.MoveTo();

                                if (AddedSource.cx > 0)
                                    return;

                                t.Stop();
                            }
                        );
                    #endregion

                    #region StartAnimatingRemove
                    Action StartAnimatingRemove =
                        delegate
                        {
                            if (Items.Count > MaxItems)
                            {
                                Items.First().With(
                                    RemovedSource =>
                                    {
                                        RemovedSource.a.SetOpacity(0,
                                            delegate
                                            {
                                                RemovedSource.Button.Orphanize();
                                                RemovedSource.Button = null;
                                            }
                                        );

                                        Items.Remove(RemovedSource);

                                    }
                                );
                            }
                        };

                    StartAnimatingRemove();
                    #endregion


                    return (RemovedSource, RemovedIndex) =>
                    {
                        //RemovedSource.Button.Orphanize();
                        //RemovedSource.Button = null;

                        if (SelectedItem == RemovedSource)
                            SelectedItem = null;

                        var u = Items.ToArray();
                        200.AtDelay(
                            delegate
                            {
                                (1000 / 20).AtIntervalWithTimerAndCounter(
                                    (t, c) =>
                                    {
                                        u.WithEach(
                                            k =>
                                            {
                                                k.x -= 4;
                                                k.MoveTo();


                                            }
                                        );

                                        if (c < 4)
                                            return;

                                        t.Stop();

                                    }
                                );
                            }
                        );
                    };
                }
            );

        }
    }
}
