import string
import functools

class Paragraph_sorter:
   
   def __init__(self):
       self.sort_order = {'A': 0, 'B': 1, 'G': 2, 'D': 3, 'H': 4, 'V': 5, 'Z' :6, 'J': 7, 'T': 8, 'Y': 9, 'K' :10, 'L': 11, 'M': 12, 'N': 13, 'S': 14, 'I': 15, 'P': 16, 'X': 17, 'Q': 18, 'R': 19, 'W': 20, 'U': 21, 'C': 22, 'E': 23, 'F': 24, 'O': 25}

   def Sort_paragraph(self, paragraph):
      clean_paragraph = self.remove_punctuation(paragraph)
      words = self.split_to_words(clean_paragraph)
      sorted_words = self.order_by_alphabet_hebrew(words)
      return self.join_to_paragraph(sorted_words)

   def remove_punctuation(self, paragraph):
      return paragraph.translate(str.maketrans('', '', string.punctuation))
   
   def split_to_words(self, txt):
      return txt.split();

   def order_by_alphabet_hebrew(self, words): 
     return sorted(words, key=functools.cmp_to_key(self.custom_sort_by_letters))
        
   def join_to_paragraph(self, words):
     return ' '.join(words)
     
   def custom_sort_by_letters(self, w1, w2):
      if len(w1) < len(w2): 
        return self.compare_letters(w2, w1) * -1 # we multiply by -1 because we revese the order of w1 and w2
      else: 
        return self.compare_letters(w1, w2) 
 
   def compare_letters(self, w1, w2):
      for i, l in enumerate(w2): # w2 is the word with the shorter len
        order_l2 = self.letter_to_order(l)
        order_l1 = self.letter_to_order(w1[i])

        if (order_l2 != order_l1):
           return order_l1 - order_l2 #compare function  l1 < l2 ==> neg, l1 > l2 ==> pos, else 0

      return 1#return 1 because w2 is sorter than w1

   def letter_to_order(self, l):
      return self.sort_order[l.upper()]


