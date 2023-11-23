INCLUDE Globals.ink

{choiceMake == "": -> main  | -> alreadyChose}

=== main ===
 (Aku berada di mana ini? Apa yang terjadi?) #speaker:Adi #potrait:Player_Calm#layout:left
 (Bagaimana bisa aku berada di hutan ini? Mungkin orang itu bisa membantuku.)
 (hmm sepertinya anak ini akan bertanya kepadaku)#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
*[ hallo... ]
    Halo, bisakah kamu memberitahuku di mana aku?#speaker:Adi #potrait:Player_Calm#layout:left
    Kamu berada di hutan dekat sungai. Tempat ini biasanya aku gunakan untuk memancing.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
*[ siapa.. ]
    Siapa namamu?#speaker:Adi #potrait:Player_Calm#layout:left
    Namaku Toba. Aku sering datang ke sini untuk memancing. Ada yang membawamu ke tempat ini?#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
*[apa yang..]
    Apa yang sedang kamu lakukan di sini?#speaker:Adi #potrait:Player_Calm#layout:left
    Aku mencoba menangkap ikan untuk makan malam. Tempat ini cukup tenang. #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
    
- Bagaimana denganmu, siapa namamu dan mengapa kamu ke wilayah sungai ini?#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
Saya Aditya, saya juga tidak mengerti bagaimana saya bisa berada di sini. #speaker:Adi #potrait:Player_Calm#layout:left
yang saya terbangun dan saya sudah berada di sini.
Kejadian yang sungguh aneh, mungkinkah ini berkaitan dengan hal mistis ? #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
Mungkin aku dan istriku bisa membantumu.
Benarkah? Kalau begitu mohon bantuannya #speaker:Adi #potrait:Player_Calm#layout:left
Sebelum itu, bisakah kamu menyampaikan pesan kepada anakku samosir?#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
Setelah itu aku akan menemuimu di rumahku, Samosir akan memberitahukan kepadamu jalannya.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right

+[Tentu]
Tentu, aku bisa membantu kamu. Apa yang kamu butuhkan? #speaker:Adi #potrait:Player_Calm#layout:left
Terima kasih! Aku sangat berterima kasih. Aku ingin kamu mengantarkan pesanku kepada anakku, Samosir.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
Dia biasanya berkeliaran di sekitar hutan ini. Tolong beri tahu dia untuk pulang.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
->chosen("Selesai")
+[Tidak Yakin]
Aku tidak yakin bisa membantu, tetapi aku akan mencoba.#speaker:Adi #potrait:Player_Calm#layout:left
Tidak masalah. Tolong, jika kamu melihat anakku, Samosir, beri tahu dia untuk pulang #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
->chosen("Selesai")
+[Tidak Mauu]
Aku berubah pikiran; aku ingin mencari jalan keluar dari tempat ini.#speaker:Adi #potrait:Player_Calm#layout:left
->chosen("Postpone")

=== chosen(pilihan) ===
~ choiceMake = pilihan
{
    - pilihan == "Selesai":
        (Membantu Toba mungkin kunci untuk memahami tempat ini dan menemukan cara pulang.)#speaker:Adi #potrait:Player_Calm#layout:left
    - pilihan == "Postpone":
        Aku mengerti. Ambil waktumu, dan jika kamu berubah pikiran lagi, aku akan di sini.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
}
-> END 

=== alreadyChose ===
bertemu lagi kita adi #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
{
	- choiceMake == "Selesai":
		Tolong beri tahu samosir untuk pulang, dan berhati hati lah kawan #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
	- else:
		Bagaimana, apakah kau bisa membantuku#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
		
}
        +[Tentu saja]
		Tentu, aku bisa membantu kamu. Apa yang kamu butuhkan?#speaker:Adi #potrait:Player_Calm#layout:left
		Terima kasih! Aku sangat berterima kasih. Aku ingin kamu mengantarkan pesanku kepada anakku, Samosir. Dia biasanya berkeliaran di sekitar hutan ini. Tolong beri tahu dia untuk pulang.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
		->chosen("Selesai")
	    +[tidak yakin]
		Aku tidak yakin bisa membantu, tetapi aku akan mencoba#speaker:Adi #potrait:Player_Calm#layout:left
		Tidak masalah. Tolong, jika kamu melihat anakku, Samosir, beri tahu dia untuk pulang.#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
		->chosen("Postpone")
    
	
->END










