import unittest
from paragraph_sorter import *

class TestParagraphSorter(unittest.TestCase):
  def setUp(self):
        self.sut = Paragraph_sorter()

  def test_empty_paragraph(self):
      self.assertEqual(self.sut.Sort_paragraph(''),'')
  def test_remove_punctuation(self):
      self.assertEqual(self.sut.Sort_paragraph('.?!,:;-[](){}`"\''), '')
  def test_sort_by_alphbet_hebrew(self):
      self.assertEqual(self.sut.Sort_paragraph('Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'), 'adipiscing aliqua amet do dolor dolore tempor labore Lorem magna sit sed incididunt ipsum ut consectetur et elit eiusmod')

if __name__ == '_main__':
   unittest.main()
