using System;
using System.Collections.Generic;

namespace KeyMapper.Classes
{
    public class PhysicalKeyboardLayout
    {
        // There are two main layouts - 'US' and 'European' (though many Europeans use the US layout..)

        // There is an extra one: the Punjabi keyboard has a European style enter key
        // and a US style left shift key 

        // Instance cache. Don't want to run through all that code every time.
        static readonly List<PhysicalKeyboardLayout> _instances = new List<PhysicalKeyboardLayout>(0);

        readonly List<KeyboardRow> _functionKeys = new List<KeyboardRow>(1);
        readonly List<KeyboardRow> _typewriterKeys = new List<KeyboardRow>(5);
        readonly List<KeyboardRow> _numberpadKeys = new List<KeyboardRow>(5);
        readonly List<KeyboardRow> _utilityKeys = new List<KeyboardRow>(1);
        readonly List<KeyboardRow> _navigationKeys = new List<KeyboardRow>(2);
        readonly List<KeyboardRow> _arrows = new List<KeyboardRow>(2);

        KeyboardLayoutType _layout;

        Boolean _isMacKeyboard;

        public IEnumerable<KeyboardRow> FunctionKeys
        { get { return this._functionKeys; } }

        public IEnumerable<KeyboardRow> TypewriterKeys
        { get { return this._typewriterKeys; } }

        public IEnumerable<KeyboardRow> NumberPadKeys
        { get { return this._numberpadKeys; } }

        public IEnumerable<KeyboardRow> UtilityKeys
        { get { return this._utilityKeys; } }

        public IEnumerable<KeyboardRow> NavigationKeys
        { get { return this._navigationKeys; } }

        public IEnumerable<KeyboardRow> Arrows
        { get { return this._arrows; } }

        private PhysicalKeyboardLayout() { }

        public static PhysicalKeyboardLayout GetPhysicalLayout(KeyboardLayoutType layout, bool isMacKeyboard)
        {

            // Look for an instance, if any. 
            foreach (PhysicalKeyboardLayout cachedLayouts in _instances)
            {
                if (cachedLayouts._layout == layout && cachedLayouts._isMacKeyboard == isMacKeyboard)
                    return cachedLayouts;
            }

            PhysicalKeyboardLayout nl = new PhysicalKeyboardLayout();

            // Assign params to new instance.
            nl._layout = layout;
            nl._isMacKeyboard = isMacKeyboard;

            nl.PopulateFunctionKeys();
            nl.PopulateArrowKeys();
            nl.PopulateNavigationKeys();
            nl.PopulateNumberpad();
            nl.PopulateUtilityKeys();

            nl.PopulateTypewriterKeys();

            _instances.Add(nl);

            return nl;
        }

        public static int[] GetRowTerminators(KeyboardLayoutType layout)
        {
            // First five rows are the hashes of the terminator keys
            // (Backspace, Enter, R Shift, R Ctrl for Euro layout)

            switch (layout)
            {
                default:
                case KeyboardLayoutType.US:
                    return new int[] { 
                        AppController.GetHashFromKeyData(14, 0), 
                        AppController.GetHashFromKeyData(43, 0), 
                        AppController.GetHashFromKeyData(28, 0), 
                        AppController.GetHashFromKeyData(54, 224), 
                        AppController.GetHashFromKeyData(29, 224) };

                case KeyboardLayoutType.Punjabi:
                case KeyboardLayoutType.European:

                    return new int[] {
                        AppController.GetHashFromKeyData(14, 0), 
                        AppController.GetHashFromKeyData(28, 0), 
                        99999,  
                        AppController.GetHashFromKeyData(54, 224), 
                        AppController.GetHashFromKeyData(29, 224) };
                        
                       

            }

        }

        private void PopulateFunctionKeys()
        {
            // Structure: 
            // KeyboardLayoutElement(scancode, extended, button, horizontalstretch, verticalstretch, rightpadding)

            if (this._isMacKeyboard)
            {
                this._functionKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                           new[]{
                                                               new KeyboardLayoutElement(1, 0, BlankButton.Blank, 0, 0, 4),  
                                                               new KeyboardLayoutElement(59, 0, BlankButton.Blank, 0, 0, 0), 
                                                               new KeyboardLayoutElement(60, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(61, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(62, 0, BlankButton.Blank, 0, 0, 1),
                                                               new KeyboardLayoutElement(63, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(64, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(65, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(66, 0, BlankButton.Blank, 0, 0, 1),
                                                               new KeyboardLayoutElement(67, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(68, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(87, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(88, 0, BlankButton.Blank, 0, 0, 0)})));


            }


            else
            {
                this._functionKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                           new[]{
                                                               new KeyboardLayoutElement(1, 0, BlankButton.Blank, 0, 0, 4),  
                                                               new KeyboardLayoutElement(59, 0, BlankButton.Blank, 0, 0, 0), 
                                                               new KeyboardLayoutElement(60, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(61, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(62, 0, BlankButton.Blank, 0, 0, 1),
                                                               new KeyboardLayoutElement(63, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(64, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(65, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(66, 0, BlankButton.Blank, 0, 0, 1),
                                                               new KeyboardLayoutElement(67, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(68, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(87, 0, BlankButton.Blank, 0, 0, 0),
                                                               new KeyboardLayoutElement(88, 0, BlankButton.Blank, 0, 0, 0)})));
            }
        }

        private void PopulateUtilityKeys()
        {

            if (this._isMacKeyboard)
                this._utilityKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(new KeyboardLayoutElement[] { null })));
            else
            {
                // PrtSc, Scroll Lock, Pause/Break
                this._utilityKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                          new[]{
                                                              new KeyboardLayoutElement(55, 224, BlankButton.Blank, 0, 0, 0), 
                                                              new KeyboardLayoutElement(70, 0, BlankButton.Blank, 0, 0, 0), 
                                                              new KeyboardLayoutElement(29, 225, BlankButton.Blank, 0, 0, 0)})));
            }
        }

        private void PopulateArrowKeys()
        {
            // Up
            this._arrows.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                            new[]{
                                                null, 
                                                new KeyboardLayoutElement(72, 224, BlankButton.Blank, 0, 0, 0)})));

            // Left, down, right
            this._arrows.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                            new[]{
                                                new KeyboardLayoutElement(75, 224, BlankButton.Blank, 0, 0, 0),
                                                new KeyboardLayoutElement(80, 224, BlankButton.Blank, 0, 0, 0), 
                                                new KeyboardLayoutElement(77, 224, BlankButton.Blank, 0, 0, 0)})));
        }

        private void PopulateNavigationKeys()
        {
            // Insert, Home, Page Up..
            this._navigationKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                         new[]{
                                                             new KeyboardLayoutElement(82, 224, BlankButton.Blank, 0, 0, 0), 
                                                             new KeyboardLayoutElement(71, 224, BlankButton.Blank, 0, 0, 0), 
                                                             new KeyboardLayoutElement(73, 224, BlankButton.Blank, 0, 0, 0)})));
            // .. Delete, End, Page Down
            this._navigationKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                         new[]{
                                                             new KeyboardLayoutElement(83, 224, BlankButton.Blank, 0, 0, 0), 
                                                             new KeyboardLayoutElement(79, 224, BlankButton.Blank, 0, 0, 0), 
                                                             new KeyboardLayoutElement(81, 224, BlankButton.Blank, 0, 0, 0)})));

        }

        private void PopulateTypewriterKeys()
        {
            // Initialise rows.

            // The first row is common to both layouts.
            // Top left key (OEM3), 1! to =+, and Backspace.

            this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                         new[]{
                                                             new KeyboardLayoutElement(41, 0, BlankButton.Blank, 0, 0, 0),  
                                                             new KeyboardLayoutElement(2, 0, BlankButton.Blank, 0, 0, 0), 
                                                             new KeyboardLayoutElement(3, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(4, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(5, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(6, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(7, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(8, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(9, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(10, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(11, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(12, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(13, 0, BlankButton.Blank, 0, 0, 0),
                                                             new KeyboardLayoutElement(14, 0, BlankButton.MediumWideBlank, 0, 0, 0)})));

            GetSecondRow();
            GetThirdRow();
            GetFourthRow();


            // Final row is same for all layouts except Macs

            if (this._isMacKeyboard)
            {
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(29, 0, BlankButton.MediumWideBlank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(56, 0, BlankButton.MediumWideBlank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(91, 224, BlankButton.MediumWideBlank, 5, 0, 1), 
                                                                 new KeyboardLayoutElement(57, 0, BlankButton.QuadrupleWideBlank, 20, 0, 0), 
                                                                 new KeyboardLayoutElement(92, 224, BlankButton.MediumWideBlank, 5, 0, 0), 
                                                                 new KeyboardLayoutElement(56, 224, BlankButton.MediumWideBlank, 0, 0, 1), 
                                                                 new KeyboardLayoutElement(29, 224, BlankButton.MediumWideBlank, 0, 0, 0)})));

            }
            else
            {
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(29, 0, BlankButton.MediumWideBlank, 0, 0, 1),
                                                                 new KeyboardLayoutElement(91, 224, BlankButton.MediumWideBlank, 0, 0, 1), 
                                                                 new KeyboardLayoutElement(56, 0, BlankButton.MediumWideBlank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(57, 0, BlankButton.QuadrupleWideBlank, 3, 0, 0), 
                                                                 new KeyboardLayoutElement(56, 224, BlankButton.MediumWideBlank, 0, 0, 1), 
                                                                 new KeyboardLayoutElement(92, 224, BlankButton.MediumWideBlank, 0, 0, 1), 
                                                                 new KeyboardLayoutElement(93, 224, BlankButton.MediumWideBlank, 0, 0, 1), 
                                                                 new KeyboardLayoutElement(29, 224, BlankButton.MediumWideBlank, 0, 0, 0)})));
            }
        }


        private void GetSecondRow()
        {
            if (this._layout == KeyboardLayoutType.US)
            {
                // Tab, Q to ]}
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(15, 0, BlankButton.MediumWideBlank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(16, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(17, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(18, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(19, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(20, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(21, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(22, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(23, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(24, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(25, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(26, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(27, 0, BlankButton.Blank, 0, 0, 2), 
                                                                 new KeyboardLayoutElement(43, 0, BlankButton.Blank, 0, 0, 0)})));

            }
            else
            {
                // Tab, Q to ]}, Enter - includes Punjabi layout.
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(15, 0, BlankButton.MediumWideBlank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(16, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(17, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(18, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(19, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(20, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(21, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(22, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(23, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(24, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(25, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(26, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(27, 0, BlankButton.Blank, 0, 0, 2), 
                                                                 new KeyboardLayoutElement(28, 0, BlankButton.TallBlank, 0, 1, 0)})));

            }
        }

        private void GetThirdRow()
        {
            if (this._layout == KeyboardLayoutType.US)
            {
                // Caps Lock, gap, A to '", double-wide enter.
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(58, 0, BlankButton.MediumWideBlank, 1, 0, 1), 
                                                                 new KeyboardLayoutElement(30, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(31, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(32, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(33, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(34, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(35, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(36, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(37, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(38, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(39, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(40, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(28, 0, BlankButton.DoubleWideBlank, 0, 0, 0)})));
            }
            else
            {
                // Caps Lock, gap, A to '@, key 43.
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(58, 0, BlankButton.MediumWideBlank, 1, 0, 1), 
                                                                 new KeyboardLayoutElement(30, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(31, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(32, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(33, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(34, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(35, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(36, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(37, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(38, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(39, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(40, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(43, 0, BlankButton.Blank, 0, 0, 0)})));
            }

        }

        private void GetFourthRow()
        {
            if ((this._layout == KeyboardLayoutType.US) | (this._layout == KeyboardLayoutType.Punjabi))
            {

                // Left Shift, Z to /?, right shift
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(42, 0, BlankButton.DoubleWideBlank, 1, 0, 0), 
                                                                 new KeyboardLayoutElement(44, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(45, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(46, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(47, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(48, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(49, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(50, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(51, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(52, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(53, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(54, 224, BlankButton.TripleWideBlank, -6, 0, 0)})));
            }
            else
            {
                // Left Shift, key 43, Z to /?, right shift
                this._typewriterKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                             new[]{
                                                                 new KeyboardLayoutElement(42, 0, BlankButton.Blank, 0, 0, 0),
                                                                 new KeyboardLayoutElement(86, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(44, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(45, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(46, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(47, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(48, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(49, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(50, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(51, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(52, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(53, 0, BlankButton.Blank, 0, 0, 0), 
                                                                 new KeyboardLayoutElement(54, 224, BlankButton.TripleWideBlank, -6, 0, 0)})));
            }

        }


        private void PopulateNumberpad()
        {

            // Now for the Numberpad: 
            // First row: NumLock / * and -
            this._numberpadKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                        new[]{
                                                            new KeyboardLayoutElement(69, 0, BlankButton.Blank, 0, 0, 0),
                                                            new KeyboardLayoutElement(53, 224, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(55, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(74, 0, BlankButton.Blank, 0, 0, 0)})));

            // Second Row: 7 8 9 +
            this._numberpadKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                        new[]{
                                                            new KeyboardLayoutElement(71, 0, BlankButton.Blank, 0, 0, 0),
                                                            new KeyboardLayoutElement(72, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(73, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(78, 0, BlankButton.TallBlank, 0, 1, 0)})));

            // Third Row: 4 5 6
            this._numberpadKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                        new[]{
                                                            new KeyboardLayoutElement(75, 0, BlankButton.Blank, 0, 0, 0),
                                                            new KeyboardLayoutElement(76, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(77, 0, BlankButton.Blank, 0, 0, 0)})));

            // Fourth Row: 1 2 3 Enter
            this._numberpadKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                        new[]{
                                                            new KeyboardLayoutElement(79, 0, BlankButton.Blank, 0, 0, 0),
                                                            new KeyboardLayoutElement(80, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(81, 0, BlankButton.Blank, 0, 0, 0), 
                                                            new KeyboardLayoutElement(28, 224, BlankButton.TallBlank, 0, 1, 0)})));

            // Finally, 0 .
            this._numberpadKeys.Add(new KeyboardRow(new List<KeyboardLayoutElement>(
                                                        new[]{
                                                            new KeyboardLayoutElement(82, 0, BlankButton.DoubleWideBlank, 0, 0, 0),
                                                            new KeyboardLayoutElement(83, 0, BlankButton.Blank, 0, 0, 0)})));
        }


    }
}