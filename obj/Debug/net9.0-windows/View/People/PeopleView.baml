   M S B A M L   `   `   `  ����A  =drakek, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:drakek.ViewModeldrakek.ViewModel  5      !    drakek.ViewModel.PeopleView   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      0localclr-namespace:drakek.ViewModel     5      .��5	      �� .+�5
   
   � -   5          �Name$  PeopleViewPanel��5
       � RowDefinitions 5      �� 5      $��5* �6    �� 5      $��* �6    .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   .+�5      �� $��PEOPLE��6   $��10, 0, 10, 0q�6)   $
��Bold"�6?   �� 5      -   5         ��Name$ 	AddPeople��5      $	��Add��5      $��10, 0, 10, 0q�5      $��75��5      5      5      � 5      $��1��6   .+�5      �� $
��Auto=�6   $
��Auto=�6D   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ PeopleTable��5         AutoGenerateColumns$ False��5         CanUserAddRows$ False��5         CanUserDeleteRows$ False��5         CanUserReorderColumns$ False��5      	   CanUserResizeColumns$	 False��5      
   CanUserResizeRows$
 False��5         CanUserSortColumns$ False��5          
IsReadOnly$
 True��5!         SelectionMode$ Single=�5"         SelectionUnit$ FullRow=�5#      $
��Left=�5$      $
��auto��5%      $��10, 10, 0, 0q�5&      $	��Top=�5'      $	��550��5(         Columns 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ No��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *index+6S     5+      $
 Name��6-   $	 300 6T    6;   �� *name+6T     5,      $
 Role��6-   $
 auto 6T    6;   �� *role+6T     5-      $ Email��6-   $
 auto 6V    6<   �� *email+6V     5.      $ Phone��6-   $
 auto 6V    6<   �� *phone+6V     5/      $ Birthday��6-   $
 auto 6\    6?   �� *
birthday+6\     50      $ Created��6-   $
 auto 6^    6>   �� *createdDate+6^   4  .System.Windows.Controls.DataGridTemplateColumn  51      $ Actions��61   $	 150 6B      CellTemplate 52      �� 53   "   .R�54   &   �� $ 
Horizontal=�61   $��Center=�6J   .+�55   *   �� -   5        $
��Edit��55   1   $	��5,0q�6@   6M   �� 56   *   -   5        $��Delete��56   1   $	��5,0q�6B   6O   57   '   58   #   5:      5<      5=      5>      5?      '   !drakek.ViewModel.PeopleUpdateForm  5@   
   -   5          Name$ PeopleUpdateForm��5@   !   $y�	Collapsed=�6;   5A      5B      