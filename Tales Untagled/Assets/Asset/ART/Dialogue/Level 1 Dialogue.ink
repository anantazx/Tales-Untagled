INCLUDE Globals.ink

{choiceMake == "": -> main  | -> alreadyChose}
=== main ===
 (Aku berada di mana ini? Apa yang terjadi?) #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
 (Bagaimana bisa aku berada di hutan ini? Mungkin orang itu bisa membantuku.)
 (hmm sepertinya anak ini akan bertanya kepadaku)#speaker:<color=\#3D689E>Toba </color> #potrait:Toba #layout:right
*[ hallo]
    Halo, bisakah kamu memberitahuku di mana aku?#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
    Kamu berada di hutan dekat sungai. Tempat ini biasanya aku gunakan untuk memancing.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
*[ siapa]
    Siapa namamu?#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
    Namaku Toba. Aku sering datang ke sini untuk memancing. Ada yang membawamu ke tempat ini?#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
*[apa yang]
    Apa yang sedang kamu lakukan di sini?#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
    Aku mencoba menangkap ikan untuk makan malam. Tempat ini cukup tenang. #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
    
- Bagaimana denganmu, siapa namamu dan mengapa kamu ke wilayah sungai ini?#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
Saya <color=\#E17220>Aditya</color>, saya juga tidak mengerti bagaimana saya bisa berada di sini. saya terbangun dan saya sudah berada di sini. #speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
Kejadian yang sungguh aneh, mungkinkah ini berkaitan dengan hal mistis ? #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
Mungkin aku dan istriku bisa membantumu.
Benarkah? Kalau begitu mohon bantuannya #speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
Sebelum itu, bisakah kamu menyampaikan pesan kepada anakku samosir?#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
Setelah itu aku akan menemuimu di rumahku, Samosir akan memberitahu kepadamu jalannya.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right

+[Tentu]
Tentu, aku bisa membantu kamu. Apa yang kamu butuhkan? #speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
Terima kasih! Aku sangat berterima kasih. Aku ingin kamu mengantarkan pesanku kepada anakku, Samosir.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
Dia biasanya berkeliaran di sekitar hutan ini. Tolong beri tahu dia untuk pulang.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
->chosen("Selesai")
+[Tidak Yakin]
Aku tidak yakin bisa membantu, tetapi aku akan mencoba.#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
Tidak masalah. Tolong, jika kamu melihat anakku, Samosir, beri tahu dia untuk pulang #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
->chosen("Selesai")
+[Tidak Mauu]
Aku berubah pikiran, aku ingin mencari jalan keluar dari tempat ini.#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
->chosen("Postpone")

=== chosen(pilihan) ===
~ choiceMake = pilihan
{
    - pilihan == "Selesai":
        (Membantu Toba mungkin kunci untuk memahami tempat ini dan menemukan cara pulang.)#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
    - pilihan == "Postpone":
        Aku mengerti. Ambil waktumu, dan jika kamu berubah pikiran lagi, aku akan di sini.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
}
-> END 

=== alreadyChose ===
bertemu lagi kita <color=\#E17220>Adi</color> #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
{
	- choiceMake == "Selesai":
		Tolong beri tahu samosir untuk pulang, dan berhati hati lah kawan #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
	- choiceMake == "Postpone":
		->ChooseNo
}
->END

=== ChooseNo ==
Bagaimana, apakah kau bisa membantuku#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
	 +[Tentu saja]
		    Tentu, aku bisa membantu kamu. Apa yang kamu butuhkan?#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
		    Terima kasih! Aku sangat berterima kasih. Aku ingin kamu mengantarkan pesanku kepada anakku, Samosir. #speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
		    Dia biasanya berkeliaran di sekitar hutan ini. Tolong beri tahu dia untuk pulang.#speaker:<color=\#3D689E>Toba</color> #potrait:Toba #layout:right
		    ->chosen("Selesai")
	  +[tidak Mau]
		    Aku tidak ingin membantuku untuk saat ini Toba#speaker:<color=\#E17220>Adi</color> #potrait:Player#layout:left
		    ->chosen("Postpone")
-> END









