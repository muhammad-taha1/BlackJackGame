   M S B A M L   `   `   `  ����H  DBlackJackGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BlackJackGameBlackJackGame  5   	       BlackJackGame.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:BlackJackGame     5   	     � Title$  
MainWindow��5   	   $��591.018��6   $��967.216��6-   
��� d � � 	Resources 5	         deal��%   &	        �� 5
   
   6   5       � Triggers 5      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� -   5         ��Name$ beginStoryboard��5      #��[  67   5      .��5      � .+�5   
   �� -   5         ��Name$ 
dealerHand��5      $
��Left=�6)   $	��123��6D   $	��Top=�6Q   $	��759��6i   ��.  �� Orientation$ 
Horizontal=�6�   $��	90,44,0,0q�6�   �� 5   
   -   5        $ 
playerHand��5      $
��Left=�6)   $	��124��6D   $	��Top=�6Q   $	��759��6i   ��. $ 
Horizontal=�6�   $��
90,346,0,0q�6�   �� 5   
   -   5         ��Name$	 hit��5      $	��Hit��6   $
��Left=�6,   $��218,493,0,0q�6G   $	��Top=�6\   $��75��6t   ���	#FFFF0B0B
���   �$
��Bold"�6�   6�   �� 5   
   -   5        $
 stay��5      $
��Stay��6   $
��Left=�6.   $��593,493,0,0q�6I   $	��Top=�6^   $��75��6v   
���   �
���  ��$
��Bold"�6�   6�   �� 5   
   -   5         ��Name$ playerScore��5      $
��Left=�6)   $��
10,370,0,0q�6D   $
��Wrap=�6X   $	��Top=�6l   $��72��6�   $��69��6�   $
��Bold"�6�   	 �� TextAlignment$	 Center=�6�   $��24(�6�   �� 5   
   -   5        $ dealerScore��5      $
��Left=�6)   $��	10,65,0,0q�6D   $
��Wrap=�6W   $	��Top=�6k   $��72��6�   $��69��6�   $
��Bold"�6�   $	 Center=�6�   $��24(�6�   �� 5   
   -   5        $ dealerMoney��5      $
��Left=�6)   $��
854,65,0,0q�6D   $
��Wrap=�6X   $	��Top=�6l   $��72��6�   $��69��6�   $
��Bold"�6�   $	 Center=�6�   $��24(�6�   �� 5   
   -	   5        $ playerMoney��5      $
��Left=�6)   $��849,370,0,0q�6D   $
��Wrap=�6Y   $	��Top=�6m   $��72��6�   $��69��6�   $
��Bold"�6�   $	 Center=�6�   $��24(�6�   5      5      